// MyActiveX.cpp : Implementation of CMyActiveXApp and DLL registration.

#include "stdafx.h"
#include "MyActiveX.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


CMyActiveXApp theApp;

const GUID CDECL _tlid = { 0x5B927B47, 0xAD8, 0x41A9, { 0x8D, 0x2D, 0x36, 0x51, 0xA, 0x8D, 0x9E, 0x9 } };
const WORD _wVerMajor = 1;
const WORD _wVerMinor = 0;



// CMyActiveXApp::InitInstance - DLL initialization

BOOL CMyActiveXApp::InitInstance()
{
	BOOL bInit = COleControlModule::InitInstance();

	if (bInit)
	{
	}

	return bInit;
}



// CMyActiveXApp::ExitInstance - DLL termination

int CMyActiveXApp::ExitInstance()
{
	// TODO: Add your own module termination code here.

	return COleControlModule::ExitInstance();
}



// DllRegisterServer - Adds entries to the system registry

STDAPI DllRegisterServer(void)
{
	AFX_MANAGE_STATE(_afxModuleAddrThis);

	if (!AfxOleRegisterTypeLib(AfxGetInstanceHandle(), _tlid))
		return ResultFromScode(SELFREG_E_TYPELIB);

	if (!COleObjectFactoryEx::UpdateRegistryAll(TRUE))
		return ResultFromScode(SELFREG_E_CLASS);

	return NOERROR;
}



// DllUnregisterServer - Removes entries from the system registry

STDAPI DllUnregisterServer(void)
{
	AFX_MANAGE_STATE(_afxModuleAddrThis);

	if (!AfxOleUnregisterTypeLib(_tlid, _wVerMajor, _wVerMinor))
		return ResultFromScode(SELFREG_E_TYPELIB);

	if (!COleObjectFactoryEx::UpdateRegistryAll(FALSE))
		return ResultFromScode(SELFREG_E_CLASS);

	return NOERROR;
}
