

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Fri Nov 27 14:35:16 2015
 */
/* Compiler settings for RPCTransport.idl:
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


#ifndef __RPCTransport_h_h__
#define __RPCTransport_h_h__

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


#ifndef __RPCTransport_INTERFACE_DEFINED__
#define __RPCTransport_INTERFACE_DEFINED__

/* interface RPCTransport */
/* [explicit_handle][version][uuid] */ 

HRESULT execute( 
    /* [in] */ handle_t IDL_handle,
    /* [in] */ long cbInput,
    /* [size_is] */ byte input[  ],
    /* [out] */ long *cbOutput,
    /* [size_is][size_is][out] */ byte **output);

/* [async] */ void  executeAsync( 
    /* [in] */ PRPC_ASYNC_STATE executeAsync_AsyncHandle,
    /* [in] */ handle_t IDL_handle,
    /* [in] */ long cbInput,
    /* [size_is] */ byte input[  ],
    /* [out] */ long *cbOutput,
    /* [size_is][size_is][out] */ byte **output);



extern RPC_IF_HANDLE RPCTransport_v1_0_c_ifspec;
extern RPC_IF_HANDLE RPCTransport_v1_0_s_ifspec;
#endif /* __RPCTransport_INTERFACE_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


