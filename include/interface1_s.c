

/* this ALWAYS GENERATED file contains the RPC server stubs */


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
#include "interface1_h.h"

#define TYPE_FORMAT_STRING_SIZE   147                               
#define PROC_FORMAT_STRING_SIZE   139                               
#define EXPR_FORMAT_STRING_SIZE   1                                 
#define TRANSMIT_AS_TABLE_SIZE    0            
#define WIRE_MARSHAL_TABLE_SIZE   1            

typedef struct _interface1_MIDL_TYPE_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ TYPE_FORMAT_STRING_SIZE ];
    } interface1_MIDL_TYPE_FORMAT_STRING;

typedef struct _interface1_MIDL_PROC_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ PROC_FORMAT_STRING_SIZE ];
    } interface1_MIDL_PROC_FORMAT_STRING;

typedef struct _interface1_MIDL_EXPR_FORMAT_STRING
    {
    long          Pad;
    unsigned char  Format[ EXPR_FORMAT_STRING_SIZE ];
    } interface1_MIDL_EXPR_FORMAT_STRING;


static const RPC_SYNTAX_IDENTIFIER  _RpcTransferSyntax = 
{{0x8A885D04,0x1CEB,0x11C9,{0x9F,0xE8,0x08,0x00,0x2B,0x10,0x48,0x60}},{2,0}};

extern const interface1_MIDL_TYPE_FORMAT_STRING interface1__MIDL_TypeFormatString;
extern const interface1_MIDL_PROC_FORMAT_STRING interface1__MIDL_ProcFormatString;
extern const interface1_MIDL_EXPR_FORMAT_STRING interface1__MIDL_ExprFormatString;

/* Standard interface: __MIDL_itf_interface1_0000_0000, ver. 0.0,
   GUID={0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}} */


/* Standard interface: AnInterface, ver. 1.0,
   GUID={0x430436ab,0x8786,0x4b19,{0x90,0x5f,0x2e,0x5c,0xc1,0x1e,0xdd,0xa2}} */


extern const MIDL_SERVER_INFO AnInterface_ServerInfo;

extern const RPC_DISPATCH_TABLE AnInterface_v1_0_DispatchTable;

static const RPC_SERVER_INTERFACE AnInterface___RpcServerInterface =
    {
    sizeof(RPC_SERVER_INTERFACE),
    {{0x430436ab,0x8786,0x4b19,{0x90,0x5f,0x2e,0x5c,0xc1,0x1e,0xdd,0xa2}},{1,0}},
    {{0x8A885D04,0x1CEB,0x11C9,{0x9F,0xE8,0x08,0x00,0x2B,0x10,0x48,0x60}},{2,0}},
    (RPC_DISPATCH_TABLE*)&AnInterface_v1_0_DispatchTable,
    0,
    0,
    0,
    &AnInterface_ServerInfo,
    0x04000000
    };
RPC_IF_HANDLE AnInterface_v1_0_s_ifspec = (RPC_IF_HANDLE)& AnInterface___RpcServerInterface;

extern const MIDL_STUB_DESC AnInterface_StubDesc;

extern const USER_MARSHAL_ROUTINE_QUADRUPLE UserMarshalRoutines[ WIRE_MARSHAL_TABLE_SIZE ];

#if !defined(__RPC_WIN32__)
#error  Invalid build platform for this stub.
#endif

#if !(TARGET_IS_NT50_OR_LATER)
#error You need Windows 2000 or later to run this stub because it uses these features:
#error   [async] attribute, /robust command line switch.
#error However, your C/C++ compilation flags indicate you intend to run this app on earlier systems.
#error This app will fail with the RPC_X_WRONG_STUB_VERSION error.
#endif


static const interface1_MIDL_PROC_FORMAT_STRING interface1__MIDL_ProcFormatString =
    {
        0,
        {

	/* Procedure Add */

			0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/*  2 */	NdrFcLong( 0x0 ),	/* 0 */
/*  6 */	NdrFcShort( 0x0 ),	/* 0 */
/*  8 */	NdrFcShort( 0x10 ),	/* x86 Stack size/offset = 16 */
/* 10 */	0x32,		/* FC_BIND_PRIMITIVE */
			0x0,		/* 0 */
/* 12 */	NdrFcShort( 0x0 ),	/* x86 Stack size/offset = 0 */
/* 14 */	NdrFcShort( 0x8 ),	/* 8 */
/* 16 */	NdrFcShort( 0x8 ),	/* 8 */
/* 18 */	0x46,		/* Oi2 Flags:  clt must size, has return, has ext, */
			0x3,		/* 3 */
/* 20 */	0x8,		/* 8 */
			0x5,		/* Ext Flags:  new corr desc, srv corr check, */
/* 22 */	NdrFcShort( 0x0 ),	/* 0 */
/* 24 */	NdrFcShort( 0x1 ),	/* 1 */
/* 26 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter IDL_handle */

/* 28 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 30 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 32 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter size */

/* 34 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 36 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 38 */	NdrFcShort( 0x3c ),	/* Type Offset=60 */

	/* Parameter pKvs */

/* 40 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 42 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 44 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure Get */


	/* Return value */

/* 46 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 48 */	NdrFcLong( 0x0 ),	/* 0 */
/* 52 */	NdrFcShort( 0x1 ),	/* 1 */
/* 54 */	NdrFcShort( 0x10 ),	/* x86 Stack size/offset = 16 */
/* 56 */	0x32,		/* FC_BIND_PRIMITIVE */
			0x0,		/* 0 */
/* 58 */	NdrFcShort( 0x0 ),	/* x86 Stack size/offset = 0 */
/* 60 */	NdrFcShort( 0x0 ),	/* 0 */
/* 62 */	NdrFcShort( 0x24 ),	/* 36 */
/* 64 */	0x45,		/* Oi2 Flags:  srv must size, has return, has ext, */
			0x3,		/* 3 */
/* 66 */	0x8,		/* 8 */
			0x3,		/* Ext Flags:  new corr desc, clt corr check, */
/* 68 */	NdrFcShort( 0x1 ),	/* 1 */
/* 70 */	NdrFcShort( 0x0 ),	/* 0 */
/* 72 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter IDL_handle */

/* 74 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 76 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 78 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pSize */

/* 80 */	NdrFcShort( 0x2013 ),	/* Flags:  must size, must free, out, srv alloc size=8 */
/* 82 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 84 */	NdrFcShort( 0x56 ),	/* Type Offset=86 */

	/* Parameter ppKvs */

/* 86 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 88 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 90 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure AsyncGet */


	/* Return value */

/* 92 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 94 */	NdrFcLong( 0x0 ),	/* 0 */
/* 98 */	NdrFcShort( 0x2 ),	/* 2 */
/* 100 */	NdrFcShort( 0x14 ),	/* x86 Stack size/offset = 20 */
/* 102 */	0x32,		/* FC_BIND_PRIMITIVE */
			0x0,		/* 0 */
/* 104 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 106 */	NdrFcShort( 0x0 ),	/* 0 */
/* 108 */	NdrFcShort( 0x24 ),	/* 36 */
/* 110 */	0xc5,		/* Oi2 Flags:  srv must size, has return, has ext, has async handle */
			0x3,		/* 3 */
/* 112 */	0x8,		/* 8 */
			0x3,		/* Ext Flags:  new corr desc, clt corr check, */
/* 114 */	NdrFcShort( 0x1 ),	/* 1 */
/* 116 */	NdrFcShort( 0x0 ),	/* 0 */
/* 118 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter IDL_handle */

/* 120 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 122 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 124 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pSize */

/* 126 */	NdrFcShort( 0x2013 ),	/* Flags:  must size, must free, out, srv alloc size=8 */
/* 128 */	NdrFcShort( 0xc ),	/* x86 Stack size/offset = 12 */
/* 130 */	NdrFcShort( 0x74 ),	/* Type Offset=116 */

	/* Parameter ppKvs */

/* 132 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 134 */	NdrFcShort( 0x10 ),	/* x86 Stack size/offset = 16 */
/* 136 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

			0x0
        }
    };

static const interface1_MIDL_TYPE_FORMAT_STRING interface1__MIDL_TypeFormatString =
    {
        0,
        {
			NdrFcShort( 0x0 ),	/* 0 */
/*  2 */	
			0x11, 0x0,	/* FC_RP */
/*  4 */	NdrFcShort( 0x38 ),	/* Offset= 56 (60) */
/*  6 */	
			0x12, 0x0,	/* FC_UP */
/*  8 */	NdrFcShort( 0xe ),	/* Offset= 14 (22) */
/* 10 */	
			0x1b,		/* FC_CARRAY */
			0x1,		/* 1 */
/* 12 */	NdrFcShort( 0x2 ),	/* 2 */
/* 14 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 16 */	NdrFcShort( 0xfffc ),	/* -4 */
/* 18 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 20 */	0x6,		/* FC_SHORT */
			0x5b,		/* FC_END */
/* 22 */	
			0x17,		/* FC_CSTRUCT */
			0x3,		/* 3 */
/* 24 */	NdrFcShort( 0x8 ),	/* 8 */
/* 26 */	NdrFcShort( 0xfff0 ),	/* Offset= -16 (10) */
/* 28 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 30 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 32 */	0xb4,		/* FC_USER_MARSHAL */
			0x83,		/* 131 */
/* 34 */	NdrFcShort( 0x0 ),	/* 0 */
/* 36 */	NdrFcShort( 0x4 ),	/* 4 */
/* 38 */	NdrFcShort( 0x0 ),	/* 0 */
/* 40 */	NdrFcShort( 0xffde ),	/* Offset= -34 (6) */
/* 42 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 44 */	NdrFcShort( 0x8 ),	/* 8 */
/* 46 */	NdrFcShort( 0x0 ),	/* 0 */
/* 48 */	NdrFcShort( 0x0 ),	/* Offset= 0 (48) */
/* 50 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 52 */	NdrFcShort( 0xffec ),	/* Offset= -20 (32) */
/* 54 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 56 */	NdrFcShort( 0xffe8 ),	/* Offset= -24 (32) */
/* 58 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 60 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 62 */	NdrFcShort( 0x0 ),	/* 0 */
/* 64 */	0x28,		/* Corr desc:  parameter, FC_LONG */
			0x0,		/*  */
/* 66 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 68 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 70 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 74 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 76 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 78 */	NdrFcShort( 0xffdc ),	/* Offset= -36 (42) */
/* 80 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 82 */	
			0x11, 0xc,	/* FC_RP [alloced_on_stack] [simple_pointer] */
/* 84 */	0x8,		/* FC_LONG */
			0x5c,		/* FC_PAD */
/* 86 */	
			0x11, 0x14,	/* FC_RP [alloced_on_stack] [pointer_deref] */
/* 88 */	NdrFcShort( 0x2 ),	/* Offset= 2 (90) */
/* 90 */	
			0x12, 0x0,	/* FC_UP */
/* 92 */	NdrFcShort( 0x2 ),	/* Offset= 2 (94) */
/* 94 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 96 */	NdrFcShort( 0x0 ),	/* 0 */
/* 98 */	0x28,		/* Corr desc:  parameter, FC_LONG */
			0x54,		/* FC_DEREFERENCE */
/* 100 */	NdrFcShort( 0x4 ),	/* x86 Stack size/offset = 4 */
/* 102 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 104 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 108 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 110 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 112 */	NdrFcShort( 0xffba ),	/* Offset= -70 (42) */
/* 114 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 116 */	
			0x11, 0x14,	/* FC_RP [alloced_on_stack] [pointer_deref] */
/* 118 */	NdrFcShort( 0x2 ),	/* Offset= 2 (120) */
/* 120 */	
			0x12, 0x0,	/* FC_UP */
/* 122 */	NdrFcShort( 0x2 ),	/* Offset= 2 (124) */
/* 124 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 126 */	NdrFcShort( 0x0 ),	/* 0 */
/* 128 */	0x28,		/* Corr desc:  parameter, FC_LONG */
			0x54,		/* FC_DEREFERENCE */
/* 130 */	NdrFcShort( 0x8 ),	/* x86 Stack size/offset = 8 */
/* 132 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 134 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 138 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 140 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 142 */	NdrFcShort( 0xff9c ),	/* Offset= -100 (42) */
/* 144 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */

			0x0
        }
    };

static const USER_MARSHAL_ROUTINE_QUADRUPLE UserMarshalRoutines[ WIRE_MARSHAL_TABLE_SIZE ] = 
        {
            
            {
            BSTR_UserSize
            ,BSTR_UserMarshal
            ,BSTR_UserUnmarshal
            ,BSTR_UserFree
            }

        };


static const unsigned short AnInterface_FormatStringOffsetTable[] =
    {
    0,
    46,
    92
    };


static const MIDL_STUB_DESC AnInterface_StubDesc = 
    {
    (void *)& AnInterface___RpcServerInterface,
    MIDL_user_allocate,
    MIDL_user_free,
    0,
    0,
    0,
    0,
    0,
    interface1__MIDL_TypeFormatString.Format,
    1, /* -error bounds_check flag */
    0x50002, /* Ndr library version */
    0,
    0x800025b, /* MIDL Version 8.0.603 */
    0,
    UserMarshalRoutines,
    0,  /* notify & notify_flag routine table */
    0x1, /* MIDL flag */
    0, /* cs routines */
    0,   /* proxy/server info */
    0
    };

static const RPC_DISPATCH_FUNCTION AnInterface_table[] =
    {
    NdrServerCall2,
    NdrServerCall2,
    NdrAsyncServerCall,
    0
    };
static const RPC_DISPATCH_TABLE AnInterface_v1_0_DispatchTable = 
    {
    3,
    (RPC_DISPATCH_FUNCTION*)AnInterface_table
    };

static const SERVER_ROUTINE AnInterface_ServerRoutineTable[] = 
    {
    (SERVER_ROUTINE)Add,
    (SERVER_ROUTINE)Get,
    (SERVER_ROUTINE)AsyncGet
    };

static const MIDL_SERVER_INFO AnInterface_ServerInfo = 
    {
    &AnInterface_StubDesc,
    AnInterface_ServerRoutineTable,
    interface1__MIDL_ProcFormatString.Format,
    AnInterface_FormatStringOffsetTable,
    0,
    0,
    0,
    0};
#pragma optimize("", on )
#if _MSC_VER >= 1200
#pragma warning(pop)
#endif


#endif /* !defined(_M_IA64) && !defined(_M_AMD64) && !defined(_ARM_) */

