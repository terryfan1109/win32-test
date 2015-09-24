// rpcClient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <iostream>
#include <string>
#include <vector>
#include <thread>

#include "RpcTransport_h.h"

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

void rpcFunc(handle_t hBinding, int cbInput, byte * input) {
	RpcTryExcept
	{
		std::wclog << L"Calling execute" << std::endl;
		long cbOutput = 0;
		byte * output = nullptr;

		HRESULT hResult = execute(hBinding, cbInput, input, &cbOutput, &output);

		std::wclog << L"result " << cbOutput << std::endl;

		std::wcin.get();
	}
	RpcExcept(1)
	{
		HandleError(L"Remote Procedure Call", RpcExceptionCode());
	}
	RpcEndExcept
}

void rpcAsyncFunc(handle_t hBinding, int cbInput, byte * input) {
	RpcTryExcept
	{
		std::wclog << L"Calling Add" << std::endl;

		RPC_ASYNC_STATE asyncState = { 0 };
		int cbAsyncState = sizeof(RPC_ASYNC_STATE);
		RPC_STATUS rt = RpcAsyncInitializeHandle(&asyncState, cbAsyncState);
		if (FAILED(rt)) {
			HandleError(L"Cannot create async state", rt);
		}
		else {
			asyncState.UserInfo = NULL;
			asyncState.NotificationType = RpcNotificationTypeEvent;
			asyncState.u.hEvent = CreateEvent(NULL, FALSE, FALSE, NULL);

			long cbOutput = 0;
			byte * output = nullptr;

			executeAsync(&asyncState, hBinding, cbInput, input, &cbOutput, &output);

			std::wcout << L"AsyncGet returned and waitting..." << std::endl;

			if (WAIT_FAILED == ::WaitForSingleObject(asyncState.u.hEvent, INFINITE)) {
				std::wcout << L"WaitForSingleObject(), failed" << std::endl;
				RpcRaiseException(::GetLastError());
			}

			HRESULT hr = S_OK;
			RpcAsyncCompleteCall(&asyncState, &hr);

			if (SUCCEEDED(hr)) {
				std::wcout << L"number: " << cbOutput << std::endl;
				::midl_user_free((LPVOID)output);
				std::wcin.get();
			}
			else {
				HandleError(L"RpcAsyncCompleteCall failed", hr);
			}
		}
	}
	RpcExcept(1)
	{
		HandleError(L"Remote Procedure Call", RpcExceptionCode());
	}
	RpcEndExcept
}

void rpcAsyncFunc2(handle_t hBinding, int cbInput, byte * input) {
	std::wclog << L"Calling rpcAsyncFunc2 >>" << std::endl;
	std::thread t([hBinding, cbInput, input]() {
		rpcAsyncFunc(hBinding, cbInput, input);
	});
	t.join();
	std::wclog << L"Calling rpcAsyncFunc2 <<" << std::endl;
}

struct RpcResponse {
	long cb;
	byte * p;
	HANDLE h;
};

void rpcAsyncFuncB(handle_t hBinding, int cbInput, byte * input) {
	RpcTryExcept
	{
		std::wclog << L"Calling Add" << std::endl;

		RPC_ASYNC_STATE asyncState = { 0 };
		int cbAsyncState = sizeof(RPC_ASYNC_STATE);
		RPC_STATUS rt = RpcAsyncInitializeHandle(&asyncState, cbAsyncState);
		if (FAILED(rt)) {
			HandleError(L"Cannot create async state", rt);
		}
		else {
			RpcResponse rpn = { 0 };
			rpn.h = CreateEvent(NULL, FALSE, FALSE, NULL);
			asyncState.UserInfo = &rpn;
			asyncState.NotificationType = RpcNotificationTypeCallback;

			asyncState.u.NotificationRoutine = [] (RPC_ASYNC_STATE * _pAsync, void * _pContext, RPC_ASYNC_EVENT _event) {
				RpcResponse * pRpn = (RpcResponse *) _pAsync->UserInfo;

				HRESULT hr = S_OK;
				RpcAsyncCompleteCall(_pAsync, &hr);

				if (SUCCEEDED(hr)) {
					std::wcout << L"number: " << pRpn->cb << std::endl;
					::midl_user_free((LPVOID)pRpn->p);
					std::wcin.get();
				}
				else {
					HandleError(L"RpcAsyncCompleteCall failed", hr);
				}

				SetEvent(pRpn->h);
			};
			executeAsync(&asyncState, hBinding, cbInput, input, &rpn.cb, &rpn.p);

			std::wcout << L"size:=" << sizeof(RPC_ASYNC_NOTIFICATION_INFO) << std::endl;
			std::wcout << L"AsyncGet returned and waitting..." << std::endl;
			::WaitForSingleObject(rpn.h, INFINITE);
			std::wcout << L"ok" << std::endl;
		}
	}
	RpcExcept(1)
	{
		HandleError(L"Remote Procedure Call", RpcExceptionCode());
	}
	RpcEndExcept
}


void rpcAsyncFuncB2(handle_t hBinding, int cbInput, byte * input) {
	std::wclog << L"Calling rpcAsyncFuncB2 >>" << std::endl;
	std::thread t([hBinding, cbInput, input]() {
		rpcAsyncFuncB(hBinding, cbInput, input);
	});
	t.join();
	std::wclog << L"Calling rpcAsyncFuncB2 <<" << std::endl;
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
		reinterpret_cast<RPC_WSTR>(L"1234"), // TCP/IP port to use.
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
	status = RpcStringFree(&szStringBinding); // String to be freed.

	if (status) {
		return HandleError(L"RpcStringFree", status);
	}

	std::wclog << L"Calling RpcEpResolveBinding" << std::endl;
	// Resolves a partially-bound server binding handle into a
	// fully-bound server binding handle.
	status = RpcEpResolveBinding(hBinding, RPCTransport_v1_0_c_ifspec);
	if (status) {
		return HandleError(L"RpcEpResolveBinding", status);
	}

	std::vector<byte> buffer(567);

	rpcAsyncFuncB2(hBinding, buffer.size(), &buffer[0]);

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
