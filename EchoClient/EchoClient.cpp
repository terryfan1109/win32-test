// EchoClient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#import "libCSharpEcho.tlb"

class ATL_NO_VTABLE ProxyCallback :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<ProxyCallback, &__uuidof(libCSharpEcho::CalculatorCallback)>,
	public libCSharpEcho::CalculatorCallback
{
public:
	ProxyCallback() {}

	DECLARE_PROTECT_FINAL_CONSTRUCT()

	BEGIN_COM_MAP(ProxyCallback)
		COM_INTERFACE_ENTRY(libCSharpEcho::CalculatorCallback)
	END_COM_MAP()

public:
	STDMETHOD(raw_callback)(long result) {
		wprintf(L"...%d", result);
		return S_OK;
	}
};


static HRESULT __stdcall MyCalculateCallback(long value) {
	wprintf(L"...%d", value);
	return S_OK;
}

void process() {
	libCSharpEcho::CalculatorPtr calculator(__uuidof(libCSharpEcho::CalculatorImpl));
	_bstr_t input = _bstr_t(L"hello!");
	int result = calculator->calculate(input);
	wprintf(L"...%d", result);

	CComObject<ProxyCallback>* pv;
	CComObject<ProxyCallback>::CreateInstance(&pv);
	libCSharpEcho::CalculatorCallbackPtr pCallback;
	pCallback.Attach(pv, false);

	calculator->asyncCalculate(pCallback, input);
}

int _tmain(int argc, _TCHAR* argv[])
{
	CoInitialize(NULL);	
	CComModule _Module;
	process();
	CoUninitialize();
	return 0;
}

