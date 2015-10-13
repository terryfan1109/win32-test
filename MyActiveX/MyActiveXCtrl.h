#pragma once

// MyActiveXCtrl.h : Declaration of the CMyActiveXCtrl ActiveX Control class.


// CMyActiveXCtrl : See MyActiveXCtrl.cpp for implementation.

class CMyActiveXCtrl : public COleControl
{
	DECLARE_DYNCREATE(CMyActiveXCtrl)

// Constructor
public:
	CMyActiveXCtrl();

// Overrides
public:
	virtual void OnDraw(CDC* pdc, const CRect& rcBounds, const CRect& rcInvalid);
	virtual void DoPropExchange(CPropExchange* pPX);
	virtual void OnResetState();

// Implementation
protected:
	~CMyActiveXCtrl();

	DECLARE_OLECREATE_EX(CMyActiveXCtrl)    // Class factory and guid
	DECLARE_OLETYPELIB(CMyActiveXCtrl)      // GetTypeInfo
	DECLARE_PROPPAGEIDS(CMyActiveXCtrl)     // Property page IDs
	DECLARE_OLECTLTYPE(CMyActiveXCtrl)		// Type name and misc status

// Message maps
	DECLARE_MESSAGE_MAP()

// Dispatch maps
	DECLARE_DISPATCH_MAP()

// Event maps
	DECLARE_EVENT_MAP()

// Dispatch and event IDs
public:
	enum {
	};
};

