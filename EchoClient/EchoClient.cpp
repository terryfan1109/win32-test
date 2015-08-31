// EchoClient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#import "libCSharpEcho.tlb"

void process() {
	libCSharpEcho::EchoPtr echo(__uuidof(libCSharpEcho::EchoImpl));
	_bstr_t input = _bstr_t(L"hello!");
	_bstr_t result = echo->_Echo(input);
	wprintf(L"...%s", (const wchar_t*) result);
}

int _tmain(int argc, _TCHAR* argv[])
{
	CoInitialize(NULL);
	process();
	CoUninitialize();
	return 0;
}

