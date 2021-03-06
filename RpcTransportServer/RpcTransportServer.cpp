// rpcServer.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <tuple>
#include <vector>
#include <iostream>
#include <string>

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

	std::wstring message(TEXT("Unknown error."));
	if (NULL != pBuffer) {
		message = std::wstring((wchar_t*)pBuffer);
	}

	std::wcerr << szFunction << TEXT(" failed. ") << message << TEXT("(") << dwError << TEXT(")") << std::endl;
	LocalFree(pBuffer);
	return dwError;
}

HRESULT execute(
	/* [in] */ handle_t IDL_handle,
	/* [in] */ long cbInput,
	/* [size_is] */ byte input[],
	/* [out] */ long *cbOutput,
	/* [size_is][size_is][out] */ byte **output)
{
	std::wclog << L"execute: " << cbInput << std::endl;

	if (0 < cbInput) {
		byte * p = (byte*) midl_user_allocate(cbInput);
		memcpy(p, input, cbInput);
		*output = p;
		*cbOutput = cbInput;
	}
	else {
		byte * p = (byte*) midl_user_allocate(1);
		memset(p, 0, 1);
		*output = p;
		*cbOutput = 0;
	}

	return S_OK;
}

void  executeAsync(
	/* [in] */ PRPC_ASYNC_STATE executeAsync_AsyncHandle,
	/* [in] */ handle_t IDL_handle,
	/* [in] */ long cbInput,
	/* [size_is] */ byte input[],
	/* [out] */ long *cbOutput,
	/* [size_is][size_is][out] */ byte **output)
{
	std::wclog << L"AsyncGet...";
	Sleep(5 * 1000);
	std::wclog << L"... wake up!" << std::endl;

	HRESULT result = S_OK;

	RPC_STATUS rt = RpcServerTestCancel(RpcAsyncGetCallHandle(executeAsync_AsyncHandle));
	if (RPC_S_OK == rt) {
		result = E_ABORT;
	}
	else if (RPC_S_CALL_IN_PROGRESS == rt) {

		if (0 < cbInput) {
			byte * p = (byte*)midl_user_allocate(cbInput);
			memcpy(p, input, cbInput);
			*output = p;
			*cbOutput = cbInput;
		}
		else {
			byte * p = (byte*)midl_user_allocate(1);
			memset(p, 0, 1);
			*output = p;
			*cbOutput = 0;
		}

		result = S_OK;
	}
	else {
		result = E_UNEXPECTED;
	}

	RpcAsyncCompleteCall(executeAsync_AsyncHandle, &result);
}


DWORD WINAPI RpcServerListenThreadProc(LPVOID /*pParam*/)
{
	// Start to listen for remote procedure calls for all registered interfaces.
	// This call will not return until RpcMgmtStopServerListening is called.
	return RpcServerListen(
		1, // Recommended minimum number of threads.
		RPC_C_LISTEN_MAX_CALLS_DEFAULT, // Recommended maximum number of threads.
		FALSE); // Start listening now.
}

RPC_STATUS CALLBACK SecurityCallback(RPC_IF_HANDLE /*hInterface*/, void* pBindingHandle)
{
	return RPC_S_OK;
}

int _tmain(int argc, _TCHAR* argv[])
{
	RPC_STATUS status;

	std::wclog << L"Calling RpcServerUseProtseqEp" << std::endl;

	status = RpcServerUseProtseqEp(
		reinterpret_cast<RPC_WSTR>(L"ncalrpc"), // Use TCP/IP protocol.
		RPC_C_PROTSEQ_MAX_REQS_DEFAULT,   // Backlog queue length for TCP/IP.
		reinterpret_cast<RPC_WSTR>(L"1234"), // TCP/IP port to use.
		NULL); // No security.

	if (status)
		return HandleError(TEXT("RpcServerUseProtseqEp"), status);

	std::wclog << TEXT("Calling RpcServerRegisterIf") << std::endl;
	// Registers the ContextExample interface.
	status = RpcServerRegisterIf2(
		RPCTransport_v1_0_s_ifspec, // Interface to register.
		NULL, // Use the MIDL generated entry-point vector.
		NULL, // Use the MIDL generated entry-point vector.
		RPC_IF_ALLOW_CALLBACKS_WITH_NO_AUTH, // Forces use of security callback.
		RPC_C_LISTEN_MAX_CALLS_DEFAULT, // Use default number of concurrent calls.
		(unsigned)-1, // Infinite max size of incoming data blocks.
		SecurityCallback); // Naive security callback.
	if (status)
		return HandleError(TEXT("RpcServerRegisterIf"), status);

	RPC_WSTR pSpn = NULL;

	status = DsServerRegisterSpn(DS_SPN_ADD_SPN_OP, TEXT("remote"), NULL);
	if (status) {
		status = RpcServerInqDefaultPrincName(RPC_C_AUTHN_GSS_NEGOTIATE, &pSpn);
		if (status) {
			return HandleError(TEXT("RpcServerInqDefaultPrincName"), status);
		}
		std::wclog << TEXT("SPN: ") << pSpn << std::endl;
	}
	else {
		std::wclog << TEXT("Creating listen thread") << std::endl;
	}

	RpcServerRegisterAuthInfo(pSpn, RPC_C_AUTHN_GSS_NEGOTIATE, NULL, NULL);
	if (status) {
		return HandleError(TEXT("RpcServerRegisterAuthInfo"), status);
	}

	std::wclog << TEXT("Creating listen thread") << std::endl;
	const HANDLE hThread = CreateThread(NULL, 0, RpcServerListenThreadProc,
		NULL, 0, NULL);
	if (!hThread)
		return HandleError(TEXT("CreateThread"), GetLastError());

	std::wcout << TEXT("Press enter to stop listening") << std::endl;
	std::wcin.get();

	std::wclog << TEXT("Calling RpcMgmtStopServerListening") << std::endl;
	status = RpcMgmtStopServerListening(NULL);
	if (status)
		return HandleError(TEXT("RpcMgmtStopServerListening"), status);

	std::wclog << TEXT("Waiting for listen thread to finish");
	while (WaitForSingleObject(hThread, 1000) == WAIT_TIMEOUT)
		std::wclog << TEXT('.');
	std::wclog << std::endl << TEXT("Listen thread finished") << std::endl;

	DWORD dwExitCodeThread = 0;
	GetExitCodeThread(hThread, &dwExitCodeThread);
	CloseHandle(hThread);
	if (dwExitCodeThread)
		return HandleError(TEXT("RpcServerListen"), dwExitCodeThread);

	std::wcout << TEXT("Press enter to exit") << std::endl;
	std::wcin.get();
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
