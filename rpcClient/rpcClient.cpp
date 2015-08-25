// rpcClient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <iostream>
#include <string>

#include "interface1_h.h"

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

void rpcFunc(handle_t hBinding, int ceKvs, KeyValuePair* kvs) {
  RpcTryExcept 
  {
    std::wclog << L"Calling Add" << std::endl;
    HRESULT hResult = Add(hBinding, ceKvs, kvs);

    std::wcout << L"Press enter to call Output:" << hResult << std::endl;
    std::wcin.get();

    std::wclog << L"Calling Output" << std::endl;
    // Calls the RPC function. The hBinding binding handle
    // is used explicitly.

    long cbBuffer = -1;
    KeyValuePair *pBuffer = NULL;
    hResult = Get(hBinding, &cbBuffer, &pBuffer);
    std::wcout << L"number" << cbBuffer << std::endl;
    for (size_t i = 0; i < cbBuffer; ++i) {
      std::wcout << (const wchar_t*) pBuffer[i].key << L": " << (const wchar_t *) pBuffer[i].value << std::endl;
    }
    ::midl_user_free((LPVOID) pBuffer);

    std::wcin.get();
  }
  RpcExcept(1)
  {
    HandleError(L"Remote Procedure Call", RpcExceptionCode());
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
      (RPC_WSTR*) &szStringBinding); // String binding output.

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
   status = RpcStringFree(
      &szStringBinding); // String to be freed.
   if (status)
      return HandleError(L"RpcStringFree", status);

   std::wclog << L"Calling RpcEpResolveBinding" << std::endl;
   // Resolves a partially-bound server binding handle into a
   // fully-bound server binding handle.
   status = RpcEpResolveBinding(hBinding, AnInterface_v1_0_c_ifspec);
   if (status) {
      return HandleError(L"RpcEpResolveBinding", status);
   }

    KeyValuePair kvs[10];
    memset(kvs, 0, sizeof(kvs));

    for (size_t i = 0; i < sizeof(kvs)/sizeof(kvs[0]); ++i) {
      kvs[i].key = ::SysAllocString((std::wstring(L"key") + std::to_wstring(i)).c_str());
      kvs[i].value = ::SysAllocString((std::wstring(L"value") + std::to_wstring(i)).c_str());
    }

    rpcFunc(hBinding, sizeof(kvs)/sizeof(kvs[0]), kvs);

    for (size_t i = 0; i < sizeof(kvs)/sizeof(kvs[0]); ++i) {
      ::SysFreeString(kvs[i].key);
      ::SysFreeString(kvs[i].value);
      kvs[i].key = nullptr;
      kvs[i].value = nullptr;
    }

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
