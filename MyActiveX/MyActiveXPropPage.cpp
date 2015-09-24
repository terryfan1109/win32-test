// MyActiveXPropPage.cpp : Implementation of the CMyActiveXPropPage property page class.

#include "stdafx.h"
#include "MyActiveX.h"
#include "MyActiveXPropPage.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

IMPLEMENT_DYNCREATE(CMyActiveXPropPage, COlePropertyPage)

// Message map

BEGIN_MESSAGE_MAP(CMyActiveXPropPage, COlePropertyPage)
END_MESSAGE_MAP()

// Initialize class factory and guid

IMPLEMENT_OLECREATE_EX(CMyActiveXPropPage, "MYACTIVEX.MyActiveXPropPage.1",
	0x2f4ebe2b, 0x486d, 0x4128, 0xb0, 0x45, 0x5, 0x56, 0x2c, 0x22, 0x5d, 0xa)

// CMyActiveXPropPage::CMyActiveXPropPageFactory::UpdateRegistry -
// Adds or removes system registry entries for CMyActiveXPropPage

BOOL CMyActiveXPropPage::CMyActiveXPropPageFactory::UpdateRegistry(BOOL bRegister)
{
	if (bRegister)
		return AfxOleRegisterPropertyPageClass(AfxGetInstanceHandle(),
			m_clsid, IDS_MYACTIVEX_PPG);
	else
		return AfxOleUnregisterClass(m_clsid, NULL);
}

// CMyActiveXPropPage::CMyActiveXPropPage - Constructor

CMyActiveXPropPage::CMyActiveXPropPage() :
	COlePropertyPage(IDD, IDS_MYACTIVEX_PPG_CAPTION)
{
}

// CMyActiveXPropPage::DoDataExchange - Moves data between page and properties

void CMyActiveXPropPage::DoDataExchange(CDataExchange* pDX)
{
	DDP_PostProcessing(pDX);
}

// CMyActiveXPropPage message handlers
