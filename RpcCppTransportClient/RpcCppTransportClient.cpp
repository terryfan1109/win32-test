// RpcCppTransportClient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <iostream>
#include <string>

#include "RPCTransport_h.h"

#include <Ntdsapi.h>

DWORD HandleError(const wchar_t* szFunction, DWORD dwError)
{
  void* pBuffer = NULL;
  FormatMessage(
    FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM |
    FORMAT_MESSAGE_IGNORE_INSERTS | FORMAT_MESSAGE_MAX_WIDTH_MASK,
    NULL,
    dwError,
    MAKELANGID(LANG_ENGLISH, SUBLANG_DEFAULT),
    LPWSTR(&pBuffer),
    0,
    NULL);

  std::wcerr << szFunction << L" failed. "
    << (pBuffer ? LPCWSTR(pBuffer) : L"Unknown error. L")
    << L"(" << dwError << L")" << std::endl;
  LocalFree(pBuffer);
  return dwError;
}

bool gFlag = false;

void rpcAsyncFunc(handle_t hBinding) {
  RpcTryExcept
  {
    while (!gFlag)
    {
      RPC_ASYNC_STATE asyncState = { 0 };
      RPC_STATUS rt = RpcAsyncInitializeHandle(&asyncState, sizeof(RPC_ASYNC_STATE));
      if (FAILED(rt)) {
        HandleError(L"Cannot create async state", rt);
      }
      else {
        asyncState.UserInfo = NULL;
        asyncState.NotificationType = RpcNotificationTypeEvent;
        asyncState.u.hEvent = CreateEvent(NULL, FALSE, FALSE, NULL);

        long cbRequest = 4;
        byte request[] = { 0, 0, 0, 0 };

        long cbBuffer = 0;
        byte* pBuffer = nullptr;

        executeAsync(&asyncState, hBinding, cbRequest, request, &cbBuffer, &pBuffer);

        std::wcout << L"AsyncGet returned and waitting..." << std::endl;

        if (WAIT_FAILED == ::WaitForSingleObject(asyncState.u.hEvent, INFINITE)) {
          std::wcout << L"WaitForSingleObject(), failed" << std::endl;
          RpcRaiseException(::GetLastError());
        }

        std::wcout << L"Received event" << std::endl;

        HRESULT hr = S_OK;
        RpcAsyncCompleteCall(&asyncState, &hr);

        if (SUCCEEDED(hr)) {
          std::wcout << L"number: " << cbBuffer << std::endl;
          ::midl_user_free((LPVOID)pBuffer);
        }
        else {
          HandleError(L"RpcAsyncCompleteCall failed", hr);
        }
      }
    }
  }
  RpcExcept(1)
  {
    HandleError(L"Remote Procedure Call", RpcExceptionCode());
  }
  RpcEndExcept
}

void rpcFunc(handle_t hBinding) {
  RpcTryExcept
  {
    std::wclog << L"Calling Add" << std::endl;

    long cbRequest = 4;
    byte request[] = { 0, 0, 0, 0 };

    long cbBuffer = 0;
    byte* pBuffer = nullptr;

    HRESULT hResult = execute(hBinding, cbRequest, request, &cbBuffer, &pBuffer);

    std::wcout << L"Press enter to call Output:" << hResult << std::endl;
    if (nullptr != pBuffer)
    {
      ::midl_user_free((LPVOID)pBuffer);
    }

    std::wcout << L"Press any to exit" << std::endl;
    std::wcin.get();
  }
  RpcExcept(1)
  {
    unsigned long error = RpcExceptionCode();
    HandleError(L"Remote Procedure Call", error);
  }
  RpcEndExcept
}

int _tmain(int argc, _TCHAR* argv[])
{
  RPC_STATUS status;
  RPC_WSTR szStringBinding = NULL;

  std::wclog << L"Calling RpcStringBindingCompose" << std::endl;

  status = RpcStringBindingCompose(
    NULL, // UUID to bind to.
    reinterpret_cast<RPC_WSTR>(L"ncalrpc"), // Use TCP/IP protocol.
    NULL,
    reinterpret_cast<RPC_WSTR>(L"4747"), // TCP/IP port to use.
    NULL, // Protocol dependent network options to use.
    (RPC_WSTR*)&szStringBinding); // String binding output.

  if (status)
    return HandleError(L"RpcStringBindingCompose", status);

  handle_t hBinding = NULL;

  std::wclog << L"Calling RpcBindingFromStringBinding" << std::endl;
  // Validates the format of the string binding handle and converts
  // it to a binding handle.
  // Connection is not done here either.
  status = RpcBindingFromStringBinding(
    szStringBinding, // The string binding to validate.
    &hBinding); // Put the result in the explicit binding handle.
  if (status)
    return HandleError(L"RpcBindingFromStringBinding", status);

  RpcBindingSetAuthInfo(hBinding, NULL, RPC_C_AUTHN_LEVEL_PKT_PRIVACY, RPC_C_AUTHN_GSS_NEGOTIATE, NULL, 0);

  std::wclog << L"Calling RpcStringFree" << std::endl;
  // Free the memory allocated by a string.
  status = RpcStringFree(&szStringBinding);
  if (status)
    return HandleError(L"RpcStringFree", status);

  std::wclog << L"Calling RpcEpResolveBinding" << std::endl;
  // Resolves a partially-bound server binding handle into a
  // fully-bound server binding handle.
  status = RpcEpResolveBinding(hBinding, RPCTransport_v1_0_c_ifspec);
  if (status) {
    return HandleError(L"RpcEpResolveBinding", status);
  }

  //rpcAsyncFunc(hBinding);
  rpcFunc(hBinding);

  std::wclog << L"Calling RpcBindingFree" << std::endl;

  // Releases binding handle resources and disconnects from the server.
  status = RpcBindingFree(&hBinding); // Frees the explicit binding handle.
  if (status)
    return HandleError(L"RpcBindingFree", status);

  std::wcout << L"Press enter to exit" << std::endl;
  std::wcin.get();

  return 0;
}

// Memory allocation function for RPC.
// The runtime uses these two functions for allocating/deallocating
// enough memory to pass the string to the server.
void* __RPC_USER midl_user_allocate(size_t size)
{
  return malloc(size);
}

// Memory deallocation function for RPC.
void __RPC_USER midl_user_free(void* p)
{
  free(p);
}

