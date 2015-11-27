

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Fri Nov 27 14:34:55 2015
 */
/* Compiler settings for interface1.idl:
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


#ifndef __interface1_h_h__
#define __interface1_h_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


/* interface __MIDL_itf_interface1_0000_0000 */
/* [local] */ 

typedef struct KeyValuePairTag
    {
    BSTR key;
    BSTR value;
    } 	KeyValuePair;



extern RPC_IF_HANDLE __MIDL_itf_interface1_0000_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_interface1_0000_0000_v0_0_s_ifspec;

#ifndef __AnInterface_INTERFACE_DEFINED__
#define __AnInterface_INTERFACE_DEFINED__

/* interface AnInterface */
/* [explicit_handle][version][uuid] */ 

HRESULT Add( 
    /* [in] */ handle_t IDL_handle,
    /* [in] */ long size,
    /* [size_is][in] */ KeyValuePair *pKvs);

HRESULT Get( 
    /* [in] */ handle_t IDL_handle,
    /* [out] */ long *pSize,
    /* [size_is][size_is][out] */ KeyValuePair **ppKvs);

/* [async] */ void  AsyncGet( 
    /* [in] */ PRPC_ASYNC_STATE AsyncGet_AsyncHandle,
    /* [in] */ handle_t IDL_handle,
    /* [out] */ long *pSize,
    /* [size_is][size_is][out] */ KeyValuePair **ppKvs);



extern RPC_IF_HANDLE AnInterface_v1_0_c_ifspec;
extern RPC_IF_HANDLE AnInterface_v1_0_s_ifspec;
#endif /* __AnInterface_INTERFACE_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long *, BSTR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


