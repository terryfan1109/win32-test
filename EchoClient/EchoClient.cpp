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

_ATL_FUNC_INFO DISP_ON_EVENT_INFO =
{
	CC_STDCALL,                                 // Calling convention.
	VT_EMPTY,                                   // Return type.
	1,                                          // Number of arguments.
	{ VT_I4 }									// VariantArgument types.
};

class ProxyEventSink :
	public IDispEventImpl<0, ProxyEventSink, &__uuidof(libCSharpEcho::CalculatorEvent)>
{
public:
	ProxyEventSink() {}

	BEGIN_SINK_MAP(ProxyEventSink)
		SINK_ENTRY_INFO(0,											// ID of event source
					    __uuidof(libCSharpEcho::CalculatorEvent),   // interface to listen to
					    0x1,										// dispatch ID to receive
					    onEvent,									// method to call when event arrives
					    &DISP_ON_EVENT_INFO)						// parameter info for method call
	END_SINK_MAP()

private:
	void __stdcall onEvent(long result) {
		wprintf(L"onEvent %d\r\n", result);
	}
};


static HRESULT __stdcall MyCalculateCallback(long value) {
	wprintf(L"...%d\r\n", value);
	return S_OK;
}

void process() {
	HRESULT hr = E_FAIL;

	libCSharpEcho::CalculatorPtr calculator(__uuidof(libCSharpEcho::CalculatorImpl));

	ProxyEventSink eventSink;
	hr = eventSink.DispEventAdvise(calculator);

	_bstr_t input = _bstr_t(L"Hello");
	int result = calculator->calculate(input);
	wprintf(L"...%d\r\n", result);

	CComObject<ProxyCallback>* pv;
	CComObject<ProxyCallback>::CreateInstance(&pv);
	libCSharpEcho::CalculatorCallbackPtr pCallback;
	pCallback.Attach(pv, false);

	calculator->asyncCalculate(pCallback, input);

	hr = eventSink.DispEventUnadvise(calculator, &eventSink.m_iid);
}

int _tmain(int argc, _TCHAR* argv[])
{
	CoInitialize(NULL);	
	CComModule _Module;
	process();
	CoUninitialize();
	return 0;
}

