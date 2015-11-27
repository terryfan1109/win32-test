

/* this ALWAYS GENERATED file contains the RPC server stubs */


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

#if !defined(_M_IA64) && !defined(_M_AMD64) && !defined(_ARM_)


#pragma warning( disable: 4049 )  /* more than 64k source lines */
#if _MSC_VER >= 1200
#pragma warning(push)
#endif

#pragma warning( disable: 4211 )  /* redefine extern to static */
#pragma warning( disable: 4232 )  /* dllimport identity*/
#pragma warning( disable: 4024 )  /* array to pointer mapping*/
#pragma warning( disable: 4100 ) /* unreferenced arguments in x86 call */

#pragma optimize("", off ) 

#include <string.h>
#include "RPCTransport_h.h"

#define TYPE_FORMAT_STRING_SIZE   71                                
#define PROC_FORMAT_STRING_SIZE   117                               
#define EXPR_FORMAT_STRING_SIZE   1                                 
#define TRANSMIT_AS_TABLE_SIZE    0            
#define WIRE_MARSHAL_TABLE_SIZE   0            

typedef struct _RPCTransport_MIDL_TYPE_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ TYPE_FORMAT_STRING_SIZE ];
    } RPCTransport_MIDL_TYPE_FORMAT_STRING;

typedef struct _RPCTransport_MIDL_PROC_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ PROC_FORMAT_STRING_SIZE ];
    } RPCTransport_MIDL_PROC_FORMAT_STRING;

typedef struct _RPCTransport_MIDL_EXPR_FORMAT_STRING
    {
    long          Pad;
    unsigned char  Format[ EXPR_FORMAT_STRING_SIZE ];
    } RPCTransport_MIDL_EXPR_FORMAT_STRING;


static const RPC_SYNTAX_IDENTIFIER  _RpcTransferSyntax = 
{{0x8A885D04,0x1CEB,0x11C9,{0x9F,0xE8,0x08,0x00,0x2B,0x10,0x48,0x60}},{2,0}};

extern const RPCTransport_MIDL_TYPE_FORMAT_STRING RPCTransport__MIDL_TypeFormatString;
extern const RPCTransport_MIDL_PROC_FORMAT_STRING RPCTransport__MIDL_ProcFormatString;
extern const RPCTransport_MIDL_EXPR_FORMAT_STRING RPCTransport__MIDL_ExprFormatString;

/* Standard interface: RPCTransport, ver. 1.0,
   GUID={0xf4db45dc,0x0dcb,0x4003,{0xb6,0x80,0x56,0xc4,0x0f,0x6c,0xb6,0xa8}} */


extern const MIDL_SERVER_INFO RPCTransport_ServerInfo;

extern const RPC_DISPATCH_TABLE RPCTransport_v1_0_DispatchTable;

static const RPC_SERVER_INTERFACE RPCTransport___RpcServerInterface =
    {
    sizeof(RPC_SERVER_INTERFACE),
    {{0xf4db45dc,0x0dcb,0x4003,{0xb6,0x80,0x56,0xc4,0x0f,0x6c,0xb6,0xa8}},{1,0}},
    {{0x8A885D04,0x1CEB,0x11C9,{0x9F,0xE8,0x08,0x00,0x2B,0x10,0x48,0x60}},{2,0}},
    (RPC_DISPATCH_TABLE*)&RPCTransport_v1_0_DispatchTable,
    0,
    0,
    0,
    &RPCTransport_ServerInfo,
    0x04000000
    };
RPC_IF_HANDLE RPCTransport_v1_0_s_ifspec = (RPC_IF_HANDLE)& RPCTransport___RpcServerInterface;

extern const MIDL_STUB_DESC RPCTransport_StubDesc;


#if !defined(__RPC_WIN32__)
#error  Invalid build platform for this stub.
#endif

#if !(TARGET_IS_NT50_OR_LATER)
#error You need Windows 2000 or later to run this stub because it uses these features:
#error   [async] attribute, /robust command line switch.
#error However, your C/C++ compilation flags indicate you intend to run this app on earlier systems.
#error This app will fail with the RPC_X_WRONG_STUB_VERSION error.
#endif


static const RPCTransport_MIDL_PROC_FORMAT_STRING RPCTransport__MIDL_ProcFormatString =
    {
        0,
        {

	/* Procedure execute */

			0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/*  2 */	NdrFcLong( 0x0 ),	/* 0 */
/*  6 */	NdrFcShort( 0x0 ),	/* 0 */
/*  8 */	NdrFcShort( 0x18 ),	/* x86 Stack size/offset = 24 */
/* 10 */	0x32,		/* FC_BIND_PRIMITIVE */
			0x0,		/* 0 */
/* 12 */	NdrFcShort( 0x0 ),	/* x86 Stack size/offset = 0 */
/* 14 */	NdrFcShort( 0x8 ),	/* 8 */
/* 16 */	NdrFcShort( 0x24 ),	/* 36 */
/* 18 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x5,		/* 5 */
/* 20 */	0x8,		/* 8 */
			0x7,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, */
/* 22 */	NdrFcShort( 0x1 ),	/* 1 */
/* 24 */	NdrFcShort( 0x1 ),	/* 1 */
/* 26 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter IDL_handle */

/* 28 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 30 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 32 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter cbInput */

/* 34 */	NdrFcShort( 0xb ),	/* Flags:  must size, must free, in, */
/* 36 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 38 */	NdrFcShort( 0x2 ),	/* Type Offset=2 */

	/* Parameter input */

/* 40 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 42 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 44 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter cbOutput */

/* 46 */	NdrFcShort( 0x2013 ),	/* Flags:  must size, must free, out, srv alloc size=8 */
/* 48 */	NdrFcShort( 0x10 ),	/* x86 Stack size/offset = 16 */
/* 50 */	NdrFcShort( 0x12 ),	/* Type Offset=18 */

	/* Parameter output */

/* 52 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 54 */	NdrFcShort( 0x14 ),	/* x86 Stack size/offset = 20 */
/* 56 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure executeAsync */


	/* Return value */

/* 58 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 60 */	NdrFcLong( 0x0 ),	/* 0 */
/* 64 */	NdrFcShort( 0x1 ),	/* 1 */
/* 66 */	NdrFcShort( 0x1c ),	/* x86 Stack size/offset = 28 */
/* 68 */	0x32,		/* FC_BIND_PRIMITIVE */
			0x0,		/* 0 */
/* 70 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 72 */	NdrFcShort( 0x8 ),	/* 8 */
/* 74 */	NdrFcShort( 0x24 ),	/* 36 */
/* 76 */	0xc7,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, has async handle */
			0x5,		/* 5 */
/* 78 */	0x8,		/* 8 */
			0x7,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, */
/* 80 */	NdrFcShort( 0x1 ),	/* 1 */
/* 82 */	NdrFcShort( 0x1 ),	/* 1 */
/* 84 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter IDL_handle */

/* 86 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 88 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 90 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter cbInput */

/* 92 */	NdrFcShort( 0xb ),	/* Flags:  must size, must free, in, */
/* 94 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 96 */	NdrFcShort( 0x26 ),	/* Type Offset=38 */

	/* Parameter input */

/* 98 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 100 */	NdrFcShort( 0x10 ),	/* x86 Stack size/offset = 16 */
/* 102 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter cbOutput */

/* 104 */	NdrFcShort( 0x2013 ),	/* Flags:  must size, must free, out, srv alloc size=8 */
/* 106 */	NdrFcShort( 0x14 ),	/* x86 Stack size/offset = 20 */
/* 108 */	NdrFcShort( 0x32 ),	/* Type Offset=50 */

	/* Parameter output */

/* 110 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 112 */	NdrFcShort( 0x18 ),	/* x86 Stack size/offset = 24 */
/* 114 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

			0x0
        }
    };

static const RPCTransport_MIDL_TYPE_FORMAT_STRING RPCTransport__MIDL_TypeFormatString =
    {
        0,
        {
			NdrFcShort( 0x0 ),	/* 0 */
/*  2 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/*  4 */	NdrFcShort( 0x1 ),	/* 1 */
/*  6 */	0x28,		/* Corr desc:  parameter, FC_LONG */
			0x0,		/*  */
/*  8 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 10 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 12 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 14 */	
			0x11, 0xc,	/* FC_RP [alloced_on_stack] [simple_pointer] */
/* 16 */	0x8,		/* FC_LONG */
			0x5c,		/* FC_PAD */
/* 18 */	
			0x11, 0x14,	/* FC_RP [alloced_on_stack] [pointer_deref] */
/* 20 */	NdrFcShort( 0x2 ),	/* Offset= 2 (22) */
/* 22 */	
			0x12, 0x0,	/* FC_UP */
/* 24 */	NdrFcShort( 0x2 ),	/* Offset= 2 (26) */
/* 26 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 28 */	NdrFcShort( 0x1 ),	/* 1 */
/* 30 */	0x28,		/* Corr desc:  parameter, FC_LONG */
			0x54,		/* FC_DEREFERENCE */
/* 32 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 34 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 36 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 38 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 40 */	NdrFcShort( 0x1 ),	/* 1 */
/* 42 */	0x28,		/* Corr desc:  parameter, FC_LONG */
			0x0,		/*  */
/* 44 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 46 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 48 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 50 */	
			0x11, 0x14,	/* FC_RP [alloced_on_stack] [pointer_deref] */
/* 52 */	NdrFcShort( 0x2 ),	/* Offset= 2 (54) */
/* 54 */	
			0x12, 0x0,	/* FC_UP */
/* 56 */	NdrFcShort( 0x2 ),	/* Offset= 2 (58) */
/* 58 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 60 */	NdrFcShort( 0x1 ),	/* 1 */
/* 62 */	0x28,		/* Corr desc:  parameter, FC_LONG */
			0x54,		/* FC_DEREFERENCE */
/* 64 */	NdrFcShort( 0x10 ),	/* x86 Stack size/offset = 16 */
/* 66 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 68 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */

			0x0
        }
    };

static const unsigned short RPCTransport_FormatStringOffsetTable[] =
    {
    0,
    58
    };


static const MIDL_STUB_DESC RPCTransport_StubDesc = 
    {
    (void *)& RPCTransport___RpcServerInterface,
    MIDL_user_allocate,
    MIDL_user_free,
    0,
    0,
    0,
    0,
    0,
    RPCTransport__MIDL_TypeFormatString.Format,
    1, /* -error bounds_check flag */
    0x50002, /* Ndr library version */
    0,
    0x800025b, /* MIDL Version 8.0.603 */
    0,
    0,
    0,  /* notify & notify_flag routine table */
    0x1, /* MIDL flag */
    0, /* cs routines */
    0,   /* proxy/server info */
    0
    };

static const RPC_DISPATCH_FUNCTION RPCTransport_table[] =
    {
    NdrServerCall2,
    NdrAsyncServerCall,
    0
    };
static const RPC_DISPATCH_TABLE RPCTransport_v1_0_DispatchTable = 
    {
    2,
    (RPC_DISPATCH_FUNCTION*)RPCTransport_table
    };

static const SERVER_ROUTINE RPCTransport_ServerRoutineTable[] = 
    {
    (SERVER_ROUTINE)execute,
    (SERVER_ROUTINE)executeAsync
    };

static const MIDL_SERVER_INFO RPCTransport_ServerInfo = 
    {
    &RPCTransport_StubDesc,
    RPCTransport_ServerRoutineTable,
    RPCTransport__MIDL_ProcFormatString.Format,
    RPCTransport_FormatStringOffsetTable,
    0,
    0,
    0,
    0};
#pragma optimize("", on )
#if _MSC_VER >= 1200
#pragma warning(pop)
#endif


#endif /* !defined(_M_IA64) && !defined(_M_AMD64) && !defined(_ARM_) */

