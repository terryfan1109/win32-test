

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Fri Nov 27 14:35:11 2015
 */
/* Compiler settings for MyActiveX.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.00.0603 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__


#ifndef __MyActiveXidl_h__
#define __MyActiveXidl_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef ___DMyActiveX_FWD_DEFINED__
#define ___DMyActiveX_FWD_DEFINED__
typedef interface _DMyActiveX _DMyActiveX;

#endif 	/* ___DMyActiveX_FWD_DEFINED__ */


#ifndef ___DMyActiveXEvents_FWD_DEFINED__
#define ___DMyActiveXEvents_FWD_DEFINED__
typedef interface _DMyActiveXEvents _DMyActiveXEvents;

#endif 	/* ___DMyActiveXEvents_FWD_DEFINED__ */


#ifndef __MyActiveX_FWD_DEFINED__
#define __MyActiveX_FWD_DEFINED__

#ifdef __cplusplus
typedef class MyActiveX MyActiveX;
#else
typedef struct MyActiveX MyActiveX;
#endif /* __cplusplus */

#endif 	/* __MyActiveX_FWD_DEFINED__ */


#ifdef __cplusplus
extern "C"{
#endif 


/* interface __MIDL_itf_MyActiveX_0000_0000 */
/* [local] */ 

#pragma once
#pragma region Desktop Family
#pragma endregion


extern RPC_IF_HANDLE __MIDL_itf_MyActiveX_0000_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_MyActiveX_0000_0000_v0_0_s_ifspec;


#ifndef __MyActiveXLib_LIBRARY_DEFINED__
#define __MyActiveXLib_LIBRARY_DEFINED__

/* library MyActiveXLib */
/* [control][version][uuid] */ 


EXTERN_C const IID LIBID_MyActiveXLib;

#ifndef ___DMyActiveX_DISPINTERFACE_DEFINED__
#define ___DMyActiveX_DISPINTERFACE_DEFINED__

/* dispinterface _DMyActiveX */
/* [uuid] */ 


EXTERN_C const IID DIID__DMyActiveX;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("315EE251-BEB4-4A46-9B1C-E427D8DEDF08")
    _DMyActiveX : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct _DMyActiveXVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            _DMyActiveX * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            _DMyActiveX * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            _DMyActiveX * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            _DMyActiveX * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            _DMyActiveX * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            _DMyActiveX * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            _DMyActiveX * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        END_INTERFACE
    } _DMyActiveXVtbl;

    interface _DMyActiveX
    {
        CONST_VTBL struct _DMyActiveXVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define _DMyActiveX_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define _DMyActiveX_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define _DMyActiveX_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define _DMyActiveX_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define _DMyActiveX_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define _DMyActiveX_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define _DMyActiveX_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* ___DMyActiveX_DISPINTERFACE_DEFINED__ */


#ifndef ___DMyActiveXEvents_DISPINTERFACE_DEFINED__
#define ___DMyActiveXEvents_DISPINTERFACE_DEFINED__

/* dispinterface _DMyActiveXEvents */
/* [uuid] */ 


EXTERN_C const IID DIID__DMyActiveXEvents;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("686DF7E4-564D-4BC7-AF78-D4854114F5A2")
    _DMyActiveXEvents : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct _DMyActiveXEventsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            _DMyActiveXEvents * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            _DMyActiveXEvents * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            _DMyActiveXEvents * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            _DMyActiveXEvents * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            _DMyActiveXEvents * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            _DMyActiveXEvents * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            _DMyActiveXEvents * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        END_INTERFACE
    } _DMyActiveXEventsVtbl;

    interface _DMyActiveXEvents
    {
        CONST_VTBL struct _DMyActiveXEventsVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define _DMyActiveXEvents_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define _DMyActiveXEvents_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define _DMyActiveXEvents_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define _DMyActiveXEvents_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define _DMyActiveXEvents_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define _DMyActiveXEvents_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define _DMyActiveXEvents_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* ___DMyActiveXEvents_DISPINTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_MyActiveX;

#ifdef __cplusplus

class DECLSPEC_UUID("28800FF0-3E5B-429F-95FD-536D3559B715")
MyActiveX;
#endif
#endif /* __MyActiveXLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


