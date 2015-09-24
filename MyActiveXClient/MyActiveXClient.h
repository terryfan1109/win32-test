
// MyActiveXClient.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CMyActiveXClientApp:
// See MyActiveXClient.cpp for the implementation of this class
//

class CMyActiveXClientApp : public CWinApp
{
public:
	CMyActiveXClientApp();

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
};

extern CMyActiveXClientApp theApp;