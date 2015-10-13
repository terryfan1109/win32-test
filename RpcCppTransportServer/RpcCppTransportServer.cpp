#include "stdafx.h"

#include <string>
#include <iostream>

#include "RPCTransport_h.h"
#include <Ntdsapi.h>

DWORD HandleError(const TCHAR* szFunction, DWORD dwError)
{
  void* pBuffer = NULL;

  FormatMessage(
    FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM,
    NULL,
    dwError,
    MAKELANGID(LANG_ENGLISH, SUBLANG_DEFAULT),
    (LPTSTR)&pBuffer,
    0,
    NULL);

  std::wstring message(L"Unknown error.");
  if (NULL != pBuffer) {
    message = std::wstring((wchar_t*)pBuffer);
  }

  std::wcerr << szFunction << L" failed. " << message << L"(" << dwError << L")" << std::endl;
  LocalFree(pBuffer);
  return dwError;
}

DWORD WINAPI RpcServerListenThreadProc(LPVOID /*pParam*/)
{
  return RpcServerListen(1, RPC_C_LISTEN_MAX_CALLS_DEFAULT, FALSE);
}

RPC_STATUS CALLBACK SecurityCallback(RPC_IF_HANDLE /*hInterface*/, void* pBindingHandle)
{
  return RPC_S_OK;
}

HRESULT execute(
  /* [in] */ handle_t IDL_handle,
  /* [in] */ long cbInput,
  /* [size_is] */ byte input[],
  /* [out] */ long *cbOutput,
  /* [size_is][size_is][out] */ byte **output)
{
  *cbOutput = 10;
  *output = (byte*)midl_user_allocate(10);
  return S_OK;
}

PRPC_ASYNC_STATE g_executeAsync_AsyncHandle;
handle_t g_IDL_handle;
long g_cbInput;
byte *g_input;
long *g_cbOutput;
byte **g_Output;

DWORD WINAPI ExecuteAsyncAction (LPVOID /*pParam*/)
{
  Sleep((rand() % 30) * 1000);

  HRESULT result;
  RPC_STATUS rt = RpcServerTestCancel(RpcAsyncGetCallHandle(g_executeAsync_AsyncHandle));
  if (RPC_S_OK == rt) {
    result = E_ABORT;
  }
  else if (RPC_S_CALL_IN_PROGRESS == rt) {
    *g_cbOutput = 10;
    *g_Output = (byte*)midl_user_allocate(10);
    result = S_OK;
  }
  else {
    result = E_UNEXPECTED;
  }

  RpcAsyncCompleteCall(g_executeAsync_AsyncHandle, &result);

  return 0;
}

void  executeAsync(
  /* [in] */ PRPC_ASYNC_STATE executeAsync_AsyncHandle,
  /* [in] */ handle_t IDL_handle,
  /* [in] */ long cbInput,
  /* [size_is] */ byte input[],
  /* [out] */ long *cbOutput,
  /* [size_is][size_is][out] */ byte **output)
{
  g_executeAsync_AsyncHandle = executeAsync_AsyncHandle;
  g_IDL_handle = IDL_handle;
  g_cbInput = cbInput;
  g_input = input;
  g_cbOutput = cbOutput;
  g_Output = output;

  const HANDLE hThread = CreateThread(NULL, 0, ExecuteAsyncAction, NULL, 0, NULL);
  std::wclog << L"executeAsync" << std::endl;
}


int _tmain(int argc, _TCHAR* argv[])
{
  RPC_STATUS status = RpcServerUseProtseqEp(
    reinterpret_cast<RPC_WSTR>(L"ncalrpc"),
    RPC_C_PROTSEQ_MAX_REQS_DEFAULT,
    reinterpret_cast<RPC_WSTR>(L"4747"),
    NULL);

  if (status) {
    return HandleError(L"RpcServerUseProtseqEp", status);
  }

  status = RpcServerRegisterIf2(
    RPCTransport_v1_0_s_ifspec, // Interface to register.
    NULL, // Use the MIDL generated entry-point vector.
    NULL, // Use the MIDL generated entry-point vector.
    RPC_IF_ALLOW_CALLBACKS_WITH_NO_AUTH, // Forces use of security callback.
    RPC_C_LISTEN_MAX_CALLS_DEFAULT, // Use default number of concurrent calls.
    (unsigned)-1, // Infinite max size of incoming data blocks.
    SecurityCallback // Naive security callback.
    );

  RPC_WSTR pSpn = NULL;

  status = DsServerRegisterSpn(DS_SPN_ADD_SPN_OP, L"remote", NULL);
  if (status) {
    status = RpcServerInqDefaultPrincName(RPC_C_AUTHN_GSS_NEGOTIATE, &pSpn);
    if (status) {
      return HandleError(L"RpcServerInqDefaultPrincName", status);
    }
    std::wclog << L"SPN: " << pSpn << std::endl;
  }
  else
  {
    std::wclog << L"Creating listen thread" << std::endl;
  }

  status = RpcServerRegisterAuthInfo(pSpn, RPC_C_AUTHN_GSS_NEGOTIATE, NULL, NULL);
  if (status) {
    return HandleError(L"RpcServerRegisterAuthInfo", status);
  }

  const HANDLE hThread = CreateThread(NULL, 0, RpcServerListenThreadProc, NULL, 0, NULL);
  if (!hThread) {
    return HandleError(L"CreateThread", GetLastError());
  }

  std::wcout << L"Press enter to stop listening" << std::endl;
  std::wcin.get();

  status = RpcMgmtStopServerListening(NULL);
  if (status)
  {
    return HandleError(L"RpcMgmtStopServerListening", status);
  }

  while (WaitForSingleObject(hThread, 1000) == WAIT_TIMEOUT)
  {
    std::wclog << L'.';
  }

  DWORD dwExitCodeThread = 0;
  GetExitCodeThread(hThread, &dwExitCodeThread);
  CloseHandle(hThread);
  if (dwExitCodeThread) {
    return HandleError(L"RpcServerListen", dwExitCodeThread);
  }

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
