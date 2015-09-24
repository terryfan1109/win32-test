// MyActiveXCtrl.cpp : Implementation of the CMyActiveXCtrl ActiveX Control class.

#include "stdafx.h"
#include "MyActiveX.h"
#include "MyActiveXCtrl.h"
#include "MyActiveXPropPage.h"
#include "afxdialogex.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

IMPLEMENT_DYNCREATE(CMyActiveXCtrl, COleControl)

// Message map

BEGIN_MESSAGE_MAP(CMyActiveXCtrl, COleControl)
	ON_OLEVERB(AFX_IDS_VERB_PROPERTIES, OnProperties)
END_MESSAGE_MAP()

// Dispatch map

BEGIN_DISPATCH_MAP(CMyActiveXCtrl, COleControl)
END_DISPATCH_MAP()

// Event map

BEGIN_EVENT_MAP(CMyActiveXCtrl, COleControl)
END_EVENT_MAP()

// Property pages

// TODO: Add more property pages as needed.  Remember to increase the count!
BEGIN_PROPPAGEIDS(CMyActiveXCtrl, 1)
	PROPPAGEID(CMyActiveXPropPage::guid)
END_PROPPAGEIDS(CMyActiveXCtrl)

// Initialize class factory and guid

IMPLEMENT_OLECREATE_EX(CMyActiveXCtrl, "MYACTIVEX.MyActiveXCtrl.1",
	0x28800ff0, 0x3e5b, 0x429f, 0x95, 0xfd, 0x53, 0x6d, 0x35, 0x59, 0xb7, 0x15)

// Type library ID and version

IMPLEMENT_OLETYPELIB(CMyActiveXCtrl, _tlid, _wVerMajor, _wVerMinor)

// Interface IDs

const IID IID_DMyActiveX = { 0x315EE251, 0xBEB4, 0x4A46, { 0x9B, 0x1C, 0xE4, 0x27, 0xD8, 0xDE, 0xDF, 0x8 } };
const IID IID_DMyActiveXEvents = { 0x686DF7E4, 0x564D, 0x4BC7, { 0xAF, 0x78, 0xD4, 0x85, 0x41, 0x14, 0xF5, 0xA2 } };

// Control type information

static const DWORD _dwMyActiveXOleMisc =
	OLEMISC_ACTIVATEWHENVISIBLE |
	OLEMISC_SETCLIENTSITEFIRST |
	OLEMISC_INSIDEOUT |
	OLEMISC_CANTLINKINSIDE |
	OLEMISC_RECOMPOSEONRESIZE;

IMPLEMENT_OLECTLTYPE(CMyActiveXCtrl, IDS_MYACTIVEX, _dwMyActiveXOleMisc)

// CMyActiveXCtrl::CMyActiveXCtrlFactory::UpdateRegistry -
// Adds or removes system registry entries for CMyActiveXCtrl

BOOL CMyActiveXCtrl::CMyActiveXCtrlFactory::UpdateRegistry(BOOL bRegister)
{
	// TODO: Verify that your control follows apartment-model threading rules.
	// Refer to MFC TechNote 64 for more information.
	// If your control does not conform to the apartment-model rules, then
	// you must modify the code below, changing the 6th parameter from
	// afxRegApartmentThreading to 0.

	if (bRegister)
		return AfxOleRegisterControlClass(
			AfxGetInstanceHandle(),
			m_clsid,
			m_lpszProgID,
			IDS_MYACTIVEX,
			IDB_MYACTIVEX,
			afxRegApartmentThreading,
			_dwMyActiveXOleMisc,
			_tlid,
			_wVerMajor,
			_wVerMinor);
	else
		return AfxOleUnregisterClass(m_clsid, m_lpszProgID);
}


// CMyActiveXCtrl::CMyActiveXCtrl - Constructor

CMyActiveXCtrl::CMyActiveXCtrl()
{
	InitializeIIDs(&IID_DMyActiveX, &IID_DMyActiveXEvents);
	// TODO: Initialize your control's instance data here.

//	CFont *m_pFont = new CFont();
//	m_pFont->CreatePointFont(22, _T("Segoe Regular"));
//	GetDlgItem(IDC_GOOGLE)->SetFont(m_pFont, TRUE);
}

// CMyActiveXCtrl::~CMyActiveXCtrl - Destructor

CMyActiveXCtrl::~CMyActiveXCtrl()
{
	// TODO: Cleanup your control's instance data here.
}

// CMyActiveXCtrl::OnDraw - Drawing function

void CMyActiveXCtrl::OnDraw(
			CDC* pdc, const CRect& rcBounds, const CRect& /* rcInvalid */)
{
	if (!pdc)
		return;

	// TODO: Replace the following code with your own drawing code.
	pdc->FillRect(rcBounds, CBrush::FromHandle((HBRUSH)GetStockObject(WHITE_BRUSH)));
	pdc->Ellipse(rcBounds);

//	CFont *pFont = new CFont();
//	pFont->CreatePointFont(22, _T("Segoe Regular"));
//	GetDlgItem(IDC_GOOGLE)->SetFont(pFont, TRUE);

}

// CMyActiveXCtrl::DoPropExchange - Persistence support

void CMyActiveXCtrl::DoPropExchange(CPropExchange* pPX)
{
	ExchangeVersion(pPX, MAKELONG(_wVerMinor, _wVerMajor));
	COleControl::DoPropExchange(pPX);

	// TODO: Call PX_ functions for each persistent custom property.
}


// CMyActiveXCtrl::OnResetState - Reset control to default state

void CMyActiveXCtrl::OnResetState()
{
	COleControl::OnResetState();  // Resets defaults found in DoPropExchange

	// TODO: Reset any other control state here.
}


// CMyActiveXCtrl message handlers
