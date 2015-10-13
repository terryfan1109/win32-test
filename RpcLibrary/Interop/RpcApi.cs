#region Copyright 2010-2014 by Roger Knapp, Licensed under the Apache License, Version 2.0
/* Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
#endregion
using System;
using System.Runtime.InteropServices;
using System.Linq;
using RpcLibrary.Interop.Structs;


namespace RpcLibrary.Interop
{
  /// <summary>
  /// WinAPI imports for RPC
  /// </summary>
  public static class RpcApi
  {
    #region MIDL_FORMAT_STRINGS

    public static readonly bool Is64BitProcess;
    public static readonly byte[] TYPE_FORMAT;
    public static readonly byte[] FUNC_FORMAT;
    public static readonly Ptr<Byte[]> FUNC_FORMAT_PTR;
    public static readonly Ptr<Byte[]> ASYNC_FUNC_FORMAT_PTR;
    public static readonly ushort[] ASYNC_FUNC_OFFSETS;

    static RpcApi()
    {
      Is64BitProcess = (IntPtr.Size == 8);
      Log.Verbose("Is64BitProcess = {0}", Is64BitProcess);

      if (Is64BitProcess)
      {
        TYPE_FORMAT = new byte[]
                    {
            0x0, 0x0, // NdrFcShort( 0x0 ),    /* 0 */
/*  2 */    
            0x1b,        /* FC_CARRAY */
            0x0,        /* 0 */
/*  4 */    0x1, 0x0, // NdrFcShort( 0x1 ),    /* 1 */
/*  6 */    0x28,        /* Corr desc:  parameter, FC_LONG */
            0x0,        /*  */
/*  8 */    0x4, 0x0, // NdrFcShort( 0x4 ),    /* x86 Stack size/offset = 4 */
/* 10 */    0x1, 0x0, // NdrFcShort( 0x1 ),    /* Corr flags:  early, */
/* 12 */    0x1,        /* FC_BYTE */
            0x5b,        /* FC_END */
/* 14 */    
            0x11, 0xc,    /* FC_RP [alloced_on_stack] [simple_pointer] */
/* 16 */    0x8,        /* FC_LONG */
            0x5c,        /* FC_PAD */
/* 18 */    
            0x11, 0x14,    /* FC_RP [alloced_on_stack] [pointer_deref] */
/* 20 */    0x2, 0x0, // NdrFcShort( 0x2 ),    /* Offset= 2 (22) */
/* 22 */    
            0x12, 0x0,    /* FC_UP */
/* 24 */    0x2, 0x0, // NdrFcShort( 0x2 ),    /* Offset= 2 (26) */
/* 26 */    
            0x1b,        /* FC_CARRAY */
            0x0,        /* 0 */
/* 28 */    0x1, 0x0, // NdrFcShort( 0x1 ),    /* 1 */
/* 30 */    0x28,        /* Corr desc:  parameter, FC_LONG */
            0x54,        /* FC_DEREFERENCE */
/* 32 */    0xc, 0x0, // NdrFcShort( 0xc ),    /* x86 Stack size/offset = 12 */
/* 34 */    0x1, 0x0, // NdrFcShort( 0x1 ),    /* Corr flags:  early, */
/* 36 */    0x1,        /* FC_BYTE */
            0x5b,        /* FC_END */
/* 38 */    
            0x1b,        /* FC_CARRAY */
            0x0,        /* 0 */
/* 40 */    0x1, 0x0, // NdrFcShort( 0x1 ),    /* 1 */
/* 42 */    0x28,        /* Corr desc:  parameter, FC_LONG */
            0x0,        /*  */
/* 44 */    0x8, 0x0, // NdrFcShort( 0x8 ),    /* x86 Stack size/offset = 8 */
/* 46 */    0x1, 0x0, // NdrFcShort( 0x1 ),    /* Corr flags:  early, */
/* 48 */    0x1,        /* FC_BYTE */
            0x5b,        /* FC_END */
/* 50 */    
            0x11, 0x14,    /* FC_RP [alloced_on_stack] [pointer_deref] */
/* 52 */    0x2, 0x0, // NdrFcShort( 0x2 ),    /* Offset= 2 (54) */
/* 54 */    
            0x12, 0x0,    /* FC_UP */
/* 56 */    0x2, 0x0, // NdrFcShort( 0x2 ),    /* Offset= 2 (58) */
/* 58 */    
            0x1b,        /* FC_CARRAY */
            0x0,        /* 0 */
/* 60 */    0x1, 0x0, // NdrFcShort( 0x1 ),    /* 1 */
/* 62 */    0x28,        /* Corr desc:  parameter, FC_LONG */
            0x54,        /* FC_DEREFERENCE */
/* 64 */    0x10, 0x0, // NdrFcShort( 0x10 ),    /* x86 Stack size/offset = 16 */
/* 66 */    0x1, 0x0,  // NdrFcShort( 0x1 ),    /* Corr flags:  early, */
/* 68 */    0x1,        /* FC_BYTE */
            0x5b,        /* FC_END */

            0x0
                    };
        FUNC_FORMAT = new byte[]
                    {
    /* Procedure execute */

            0x0,        /* 0 */
            0x48,        /* Old Flags:  */
/*  2 */    0x0, 0x0, 0x0, 0x0, // NdrFcLong( 0x0 ),    /* 0 */
/*  6 */    0x0, 0x0,  // NdrFcShort( 0x0 ),    /* 0 */
/*  8 */    0x30, 0x0, // NdrFcShort( 0x30 ),    /* X64 Stack size/offset = 48 */
/* 10 */    0x32,        /* FC_BIND_PRIMITIVE */
            0x0,        /* 0 */
/* 12 */    0x0, 0x0,  // NdrFcShort( 0x0 ),    /* X64 Stack size/offset = 0 */
/* 14 */    0x8, 0x0,  // NdrFcShort( 0x8 ),    /* 8 */
/* 16 */    0x24, 0x0, // NdrFcShort( 0x24 ),    /* 36 */
/* 18 */    0x47,        /* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
            0x5,        /* 5 */
/* 20 */    0xa,        /* 10 */
            0x7,        /* Ext Flags:  new corr desc, clt corr check, srv corr check, */
/* 22 */    0x1, 0x0,  // NdrFcShort( 0x1 ),    /* 1 */
/* 24 */    0x1, 0x0,  // NdrFcShort( 0x1 ),    /* 1 */
/* 26 */    0x0, 0x0,  // NdrFcShort( 0x0 ),    /* 0 */
/* 28 */    0x0, 0x0,  // NdrFcShort( 0x0 ),    /* 0 */

    /* Parameter IDL_handle */

/* 30 */    0x48, 0x0, // NdrFcShort( 0x48 ),    /* Flags:  in, base type, */
/* 32 */    0x8, 0x0,  // NdrFcShort( 0x8 ),    /* X64 Stack size/offset = 8 */
/* 34 */    0x8,        /* FC_LONG */
            0x0,        /* 0 */

    /* Parameter cbInput */

/* 36 */    0xb, 0x0,  // NdrFcShort( 0xb ),    /* Flags:  must size, must free, in, */
/* 38 */    0x10, 0x0, // NdrFcShort( 0x10 ),    /* X64 Stack size/offset = 16 */
/* 40 */    0x2, 0x0,  // NdrFcShort( 0x2 ),    /* Type Offset=2 */

    /* Parameter input */

/* 42 */    0x50, 0x21, // NdrFcShort( 0x2150 ),    /* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 44 */    0x18, 0x0,  // NdrFcShort( 0x18 ),    /* X64 Stack size/offset = 24 */
/* 46 */    0x8,        /* FC_LONG */
                0x0,        /* 0 */

    /* Parameter cbOutput */

/* 48 */    0x13, 0x20, // NdrFcShort( 0x2013 ),    /* Flags:  must size, must free, out, srv alloc size=8 */
/* 50 */    0x20, 0x0,  // NdrFcShort( 0x20 ),    /* X64 Stack size/offset = 32 */
/* 52 */    0x12, 0x0,  // NdrFcShort( 0x12 ),    /* Type Offset=18 */

    /* Parameter output */

/* 54 */    0x70, 0x0,  // NdrFcShort( 0x70 ),    /* Flags:  out, return, base type, */
/* 56 */    0x28, 0x0,  // NdrFcShort( 0x28 ),    /* X64 Stack size/offset = 40 */
/* 58 */    0x8,        /* FC_LONG */
                0x0,        /* 0 */

    /* Procedure executeAsync */


    /* Return value */

/* 60 */    0x0,        /* 0 */
            0x48,        /* Old Flags:  */
/* 62 */    0x0, 0x0, 0x0, 0x0, // NdrFcLong( 0x0 ),    /* 0 */
/* 66 */    0x1, 0x0,  // NdrFcShort( 0x1 ),    /* 1 */
/* 68 */    0x38, 0x0, // NdrFcShort( 0x38 ),    /* X64 Stack size/offset = 56 */
/* 70 */    0x32,        /* FC_BIND_PRIMITIVE */
            0x0,        /* 0 */
/* 72 */    0x8, 0x0,  // NdrFcShort( 0x8 ),    /* X64 Stack size/offset = 8 */
/* 74 */    0x8, 0x0,  // NdrFcShort( 0x8 ),    /* 8 */
/* 76 */    024, 0x0,  // NdrFcShort( 0x24 ),    /* 36 */
/* 78 */    0xc7,        /* Oi2 Flags:  srv must size, clt must size, has return, has ext, has async handle */
            0x5,        /* 5 */
/* 80 */    0xa,        /* 10 */
            0x7,        /* Ext Flags:  new corr desc, clt corr check, srv corr check, */
/* 82 */    0x1, 0x0,  // NdrFcShort( 0x1 ),    /* 1 */
/* 84 */    0x1, 0x0,  // NdrFcShort( 0x1 ),    /* 1 */
/* 86 */    0x0, 0x0,  // NdrFcShort( 0x0 ),    /* 0 */
/* 88 */    0x0, 0x0,  // NdrFcShort( 0x0 ),    /* 0 */

    /* Parameter IDL_handle */

/* 90 */    0x48, 0x0, // NdrFcShort( 0x48 ),    /* Flags:  in, base type, */
/* 92 */    0x10, 0x0, // NdrFcShort( 0x10 ),    /* X64 Stack size/offset = 16 */
/* 94 */    0x8,        /* FC_LONG */
            0x0,        /* 0 */

    /* Parameter cbInput */

/* 96 */    0xb, 0x0,  // NdrFcShort( 0xb ),    /* Flags:  must size, must free, in, */
/* 98 */    0x18, 0x0, // NdrFcShort( 0x18 ),    /* X64 Stack size/offset = 24 */
/* 100 */    0x26, 0x0, // NdrFcShort( 0x26 ),    /* Type Offset=38 */

    /* Parameter input */

/* 102 */    0x50, 0x21, // NdrFcShort( 0x2150 ),    /* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 104 */    0x20, 0x0,  // NdrFcShort( 0x20 ),    /* X64 Stack size/offset = 32 */
/* 106 */    0x8,        /* FC_LONG */
            0x0,        /* 0 */

    /* Parameter cbOutput */

/* 108 */    0x13, 0x20, // NdrFcShort( 0x2013 ),    /* Flags:  must size, must free, out, srv alloc size=8 */
/* 110 */    0x28, 0x0,  // NdrFcShort( 0x28 ),    /* X64 Stack size/offset = 40 */
/* 112 */    0x32, 0x0,  // NdrFcShort( 0x32 ),    /* Type Offset=50 */

    /* Parameter output */

/* 114 */    0x70, 0x0,  // NdrFcShort( 0x70 ),    /* Flags:  out, return, base type, */
/* 116 */    0x30, 0x0,  // NdrFcShort( 0x30 ),    /* X64 Stack size/offset = 48 */
/* 118 */    0x8,        /* FC_LONG */
            0x0,        /* 0 */

            0x0
                    };
        ASYNC_FUNC_OFFSETS = new ushort[] { 0, 60 };
      }
      else
      {
        TYPE_FORMAT = new byte[]
                    {
            0x0, 0x0,   // NdrFcShort( 0x0 ),    /* 0 */
/*  2 */    
            0x1b,       /* FC_CARRAY */
            0x0,        /* 0 */
/*  4 */    0x1, 0x0,   // NdrFcShort( 0x1 ),    /* 1 */
/*  6 */    0x28,       /* Corr desc:  parameter, FC_LONG */
            0x0,        /*  */
/*  8 */    0x4, 0x0,   // NdrFcShort( 0x4 ),    /* x86 Stack size/offset = 4 */
/* 10 */    0x1, 0x0,   // NdrFcShort( 0x1 ),    /* Corr flags:  early, */
/* 12 */    0x1,        /* FC_BYTE */
            0x5b,       /* FC_END */
/* 14 */    
            0x11, 0xc,  /* FC_RP [alloced_on_stack] [simple_pointer] */
/* 16 */    0x8,        /* FC_LONG */
            0x5c,       /* FC_PAD */
/* 18 */    
            0x11, 0x14, /* FC_RP [alloced_on_stack] [pointer_deref] */
/* 20 */    0x2, 0x0,   // NdrFcShort( 0x2 ),    /* Offset= 2 (22) */
/* 22 */    
            0x12, 0x0,  /* FC_UP */
/* 24 */    0x2, 0x0,   // NdrFcShort( 0x2 ),    /* Offset= 2 (26) */
/* 26 */    
            0x1b,       /* FC_CARRAY */
            0x0,        /* 0 */
/* 28 */    0x1, 0x0,   // NdrFcShort( 0x1 ),    /* 1 */
/* 30 */    0x28,       /* Corr desc:  parameter, FC_LONG */
            0x54,       /* FC_DEREFERENCE */
/* 32 */    0xc, 0x0,   // NdrFcShort( 0xc ),    /* x86 Stack size/offset = 12 */
/* 34 */    0x1, 0x0,   // NdrFcShort( 0x1 ),    /* Corr flags:  early, */
/* 36 */    0x1,        /* FC_BYTE */
            0x5b,       /* FC_END */
/* 38 */    
            0x1b,       /* FC_CARRAY */
            0x0,        /* 0 */
/* 40 */    0x1, 0x0,   // NdrFcShort( 0x1 ),    /* 1 */
/* 42 */    0x28,       /* Corr desc:  parameter, FC_LONG */
            0x0,        /*  */
/* 44 */    0x8, 0x0,   // NdrFcShort( 0x8 ),    /* x86 Stack size/offset = 8 */
/* 46 */    0x1, 0x0,   // NdrFcShort( 0x1 ),    /* Corr flags:  early, */
/* 48 */    0x1,        /* FC_BYTE */
            0x5b,       /* FC_END */
/* 50 */    
            0x11, 0x14, /* FC_RP [alloced_on_stack] [pointer_deref] */
/* 52 */    0x2, 0x0,   // NdrFcShort( 0x2 ),    /* Offset= 2 (54) */
/* 54 */    
            0x12, 0x0,  /* FC_UP */
/* 56 */    0x2, 0x0,   // NdrFcShort( 0x2 ),    /* Offset= 2 (58) */
/* 58 */    
            0x1b,       /* FC_CARRAY */
            0x0,        /* 0 */
/* 60 */    0x1, 0x0,   // NdrFcShort( 0x1 ),    /* 1 */
/* 62 */    0x28,       /* Corr desc:  parameter, FC_LONG */
            0x54,       /* FC_DEREFERENCE */
/* 64 */    0x10, 0x0,  // NdrFcShort( 0x10 ),    /* x86 Stack size/offset = 16 */
/* 66 */    0x1, 0x0,   // NdrFcShort( 0x1 ),    /* Corr flags:  early, */
/* 68 */    0x1,        /* FC_BYTE */
            0x5b,       /* FC_END */

            0x0
                    };
        FUNC_FORMAT = new byte[]
                    {

    /* Procedure execute */

            0x0,        /* 0 */
            0x48,       /* Old Flags:  */
/*  2 */    0x0, 0x0, 0x0, 0x0, // NdrFcLong( 0x0 ),    /* 0 */
/*  6 */    0x0, 0x0,   // NdrFcShort( 0x0 ),    /* 0 */
/*  8 */    0x18, 0x0,  // NdrFcShort( 0x18 ),    /* x86 Stack size/offset = 24 */
/* 10 */    0x32,       /* FC_BIND_PRIMITIVE */
            0x0,        /* 0 */
/* 12 */    0x0, 0x0,   // NdrFcShort( 0x0 ),    /* x86 Stack size/offset = 0 */
/* 14 */    0x8, 0x0,   // NdrFcShort( 0x8 ),    /* 8 */
/* 16 */    0x24, 0x0,  // NdrFcShort( 0x24 ),    /* 36 */
/* 18 */    0x47,       /* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
            0x5,        /* 5 */
/* 20 */    0x8,        /* 8 */
            0x7,        /* Ext Flags:  new corr desc, clt corr check, srv corr check, */
/* 22 */    0x1, 0x0,   // NdrFcShort( 0x1 ),    /* 1 */
/* 24 */    0x1, 0x0,   // NdrFcShort( 0x1 ),    /* 1 */
/* 26 */    0x0, 0x0,   // NdrFcShort( 0x0 ),    /* 0 */

    /* Parameter IDL_handle */

/* 28 */    0x48, 0x0,  // NdrFcShort( 0x48 ),    /* Flags:  in, base type, */
/* 30 */    0x4, 0x0,   // NdrFcShort( 0x4 ),    /* x86 Stack size/offset = 4 */
/* 32 */    0x8,        /* FC_LONG */
            0x0,        /* 0 */

    /* Parameter cbInput */

/* 34 */    0xb, 0x0,  // NdrFcShort( 0xb ),    /* Flags:  must size, must free, in, */
/* 36 */    0x8, 0x0,  // NdrFcShort( 0x8 ),    /* x86 Stack size/offset = 8 */
/* 38 */    0x2, 0x0,  // NdrFcShort( 0x2 ),    /* Type Offset=2 */

    /* Parameter input */

/* 40 */    0x50, 0x21, // NdrFcShort( 0x2150 ),    /* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 42 */    0xc, 0x0,   // NdrFcShort( 0xc ),    /* x86 Stack size/offset = 12 */
/* 44 */    0x8,        /* FC_LONG */
            0x0,        /* 0 */

    /* Parameter cbOutput */

/* 46 */    0x13, 0x20, // NdrFcShort( 0x2013 ),    /* Flags:  must size, must free, out, srv alloc size=8 */
/* 48 */    0x10, 0x0,  // NdrFcShort( 0x10 ),    /* x86 Stack size/offset = 16 */
/* 50 */    0x12, 0x0,  // NdrFcShort( 0x12 ),    /* Type Offset=18 */

    /* Parameter output */

/* 52 */    0x70, 0x0, // NdrFcShort( 0x70 ),    /* Flags:  out, return, base type, */
/* 54 */    0x14, 0x0, // NdrFcShort( 0x14 ),    /* x86 Stack size/offset = 20 */
/* 56 */    0x8,        /* FC_LONG */
            0x0,        /* 0 */

    /* Procedure executeAsync */


    /* Return value */

/* 58 */    0x0,       /* 0 */
            0x48,      /* Old Flags:  */
/* 60 */    0x0, 0x0, 0x0, 0x0, // NdrFcLong( 0x0 ),    /* 0 */
/* 64 */    0x1, 0x0,  // NdrFcShort( 0x1 ),    /* 1 */
/* 66 */    0x1c, 0x0, // NdrFcShort( 0x1c ),    /* x86 Stack size/offset = 28 */
/* 68 */    0x32,      /* FC_BIND_PRIMITIVE */
            0x0,       /* 0 */
/* 70 */    0x4, 0x0,  // NdrFcShort( 0x4 ),    /* x86 Stack size/offset = 4 */
/* 72 */    0x8, 0x0,  // NdrFcShort( 0x8 ),    /* 8 */
/* 74 */    0x24, 0x0, // NdrFcShort( 0x24 ),    /* 36 */
/* 76 */    0xc7,      /* Oi2 Flags:  srv must size, clt must size, has return, has ext, has async handle */
            0x5,       /* 5 */
/* 78 */    0x8,       /* 8 */
            0x7,       /* Ext Flags:  new corr desc, clt corr check, srv corr check, */
/* 80 */    0x1, 0x0,  // NdrFcShort( 0x1 ),    /* 1 */
/* 82 */    0x1, 0x0,  // NdrFcShort( 0x1 ),    /* 1 */
/* 84 */    0x0, 0x0,  // NdrFcShort( 0x0 ),    /* 0 */

    /* Parameter IDL_handle */

/* 86 */    0x48, 0x0, // NdrFcShort( 0x48 ),    /* Flags:  in, base type, */
/* 88 */    0x8, 0x0,  // NdrFcShort( 0x8 ),    /* x86 Stack size/offset = 8 */
/* 90 */    0x8,       /* FC_LONG */
            0x0,       /* 0 */

    /* Parameter cbInput */

/* 92 */    0xb, 0x0,  // NdrFcShort( 0xb ),    /* Flags:  must size, must free, in, */
/* 94 */    0xc, 0x0,  // NdrFcShort( 0xc ),    /* x86 Stack size/offset = 12 */
/* 96 */    0x26, 0x0, // NdrFcShort( 0x26 ),    /* Type Offset=38 */

    /* Parameter input */

/* 98 */    0x50, 0x21, // NdrFcShort( 0x2150 ),    /* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 100 */   0x10, 0x0,  // NdrFcShort( 0x10 ),    /* x86 Stack size/offset = 16 */
/* 102 */   0x8,        /* FC_LONG */
            0x0,        /* 0 */

    /* Parameter cbOutput */

/* 104 */   0x13, 0x20, // NdrFcShort( 0x2013 ),    /* Flags:  must size, must free, out, srv alloc size=8 */
/* 106 */   0x14, 0x0,  // NdrFcShort( 0x14 ),    /* x86 Stack size/offset = 20 */
/* 108 */   0x32, 0x0,  // NdrFcShort( 0x32 ),    /* Type Offset=50 */

    /* Parameter output */

/* 110 */   0x70, 0x0,  // NdrFcShort( 0x70 ),    /* Flags:  out, return, base type, */
/* 112 */   0x18, 0x0,  // NdrFcShort( 0x18 ),    /* x86 Stack size/offset = 24 */
/* 114 */   0x8,        /* FC_LONG */
            0x0,        /* 0 */

            0x0
                };
        ASYNC_FUNC_OFFSETS = new ushort[] { 0, 58 };
      }
      FUNC_FORMAT_PTR = new Ptr<byte[]>(FUNC_FORMAT);
      ASYNC_FUNC_FORMAT_PTR = new Ptr<byte[]>(
        FUNC_FORMAT.Skip(ASYNC_FUNC_OFFSETS[1]).Take(FUNC_FORMAT.Length - ASYNC_FUNC_OFFSETS[1]).ToArray());
    }


    #endregion

    #region Memory Utils

    [DllImport("Kernel32.dll", EntryPoint = "LocalFree", SetLastError = true,
        CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
    private static extern IntPtr LocalFree(IntPtr memHandle);

    public static void Free(IntPtr ptr)
    {
      if (ptr != IntPtr.Zero)
      {
        Log.Verbose("LocalFree({0})", ptr);
        LocalFree(ptr);
      }
    }

    private const UInt32 LPTR = 0x0040;

    [DllImport("Kernel32.dll", EntryPoint = "LocalAlloc", SetLastError = true,
        CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
    private static extern IntPtr LocalAlloc(UInt32 flags, UInt32 nBytes);

    public static IntPtr Alloc(uint size)
    {
      IntPtr ptr = LocalAlloc(LPTR, size);
      Log.Verbose("{0} = LocalAlloc({1})", ptr, size);
      return ptr;
    }

    [DllImport("Rpcrt4.dll", EntryPoint = "NdrServerCall2", CallingConvention = CallingConvention.StdCall,
        CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern void NdrServerCall2(IntPtr ptr);

    [DllImport("Rpcrt4.dll", EntryPoint = "NdrAsyncServerCall", CallingConvention = CallingConvention.StdCall,
        CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern void NdrAsyncServerCall(IntPtr ptr);

    public delegate void ServerEntryPoint(IntPtr ptr);
    public delegate void AsyncServerEntryPoint(IntPtr ptr);

    public static FunctionPtr<ServerEntryPoint> ServerEntry = new FunctionPtr<ServerEntryPoint>(NdrServerCall2);
    public static FunctionPtr<AsyncServerEntryPoint> AsyncServerEntry = new FunctionPtr<AsyncServerEntryPoint>(NdrAsyncServerCall);

    public static FunctionPtr<LocalAlloc> AllocPtr = new FunctionPtr<LocalAlloc>(Alloc);
    public static FunctionPtr<LocalFree> FreePtr = new FunctionPtr<LocalFree>(Free);

    #endregion

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcServerUnregisterIf", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcServerUnregisterIf(IntPtr IfSpec, IntPtr MgrTypeUuid, uint WaitForCallsToComplete);

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcServerUseProtseqEpW", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcServerUseProtseqEp(String Protseq, int MaxCalls, String Endpoint, IntPtr SecurityDescriptor);

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcServerRegisterIf2", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcServerRegisterIf2(IntPtr IfSpec, IntPtr MgrTypeUuid, IntPtr MgrEpv, int Flags, int MaxCalls, int MaxRpcSize, IntPtr hProc);

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcServerRegisterIf", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcServerRegisterIf(IntPtr IfSpec, IntPtr MgrTypeUuid, IntPtr MgrEpv);

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcServerRegisterAuthInfoW",
        CallingConvention = CallingConvention.StdCall,
        CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcServerRegisterAuthInfo(String ServerPrincName, uint AuthnSvc, IntPtr GetKeyFn, IntPtr Arg);

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcServerListen", CallingConvention = CallingConvention.StdCall,
        CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcServerListen(uint MinimumCallThreads, int MaxCalls, uint DontWait);

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcMgmtStopServerListening",
        CallingConvention = CallingConvention.StdCall,
        CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcMgmtStopServerListening(IntPtr ignore);

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcMgmtWaitServerListen", CallingConvention = CallingConvention.StdCall,
        CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcMgmtWaitServerListen();

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcAsyncGetCallStatus", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcAsyncGetCallStatus(IntPtr pAsyncState);

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcAsyncCompleteCall", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcAsyncCompleteCall(IntPtr pAsyncState, ref IntPtr result);

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcAsyncAbortCall", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcAsyncCancelCall(IntPtr pAsyncState, bool fAbort);

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcAsyncCancelCall", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcAsyncAbortCall(IntPtr pAsyncState, uint result);

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcServerTestCancel", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcServerTestCancel(IntPtr bindingHandle);

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcStringFreeW", CallingConvention = CallingConvention.StdCall,
        CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcStringFree(ref IntPtr lpString);

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingFree", CallingConvention = CallingConvention.StdCall,
        CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcBindingFree(ref IntPtr lpString);

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcStringBindingComposeW", CallingConvention = CallingConvention.StdCall,
        CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcStringBindingCompose(
        String ObjUuid, String ProtSeq, String NetworkAddr, String Endpoint, String Options,
        out IntPtr lpBindingString
        );

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingFromStringBindingW",
        CallingConvention = CallingConvention.StdCall,
        CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcBindingFromStringBinding(String bindingString, out IntPtr lpBinding);

    [DllImport("Rpcrt4.dll", EntryPoint = "NdrClientCall2", CallingConvention = CallingConvention.Cdecl,
        CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr NdrClientCall2x86(IntPtr pMIDL_STUB_DESC, IntPtr formatString, IntPtr args);

    [DllImport("Rpcrt4.dll", EntryPoint = "NdrClientCall2", CallingConvention = CallingConvention.Cdecl,
        CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr NdrClientCall2x64(IntPtr pMIDL_STUB_DESC, IntPtr formatString, IntPtr Handle,
                                                   int DataSize, IntPtr Data, [Out] out int ResponseSize,
                                                   [Out] out IntPtr Response);

    [DllImport("Rpcrt4.dll", EntryPoint = "NdrAsyncClientCall", CallingConvention = CallingConvention.Cdecl,
        CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr NdrAsyncClientCallx86(IntPtr pMIDL_STUB_DESC, IntPtr formatString, IntPtr args);

    [DllImport("Rpcrt4.dll", EntryPoint = "NdrAsyncClientCall", CallingConvention = CallingConvention.Cdecl,
        CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr NdrAsyncClientCallx64(IntPtr pMIDL_STUB_DESC, IntPtr formatString, IntPtr asyncHandle,
                                                       IntPtr Handle, int cbInput, IntPtr input, [Out] out int cbOutput,
                                                       [Out] out IntPtr Response);

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcAsyncInitializeHandle", CallingConvention = CallingConvention.StdCall,
        CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr RpcAsyncInitializeHandle(IntPtr pAsync, int cbAsync);

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingSetAuthInfoW", CallingConvention = CallingConvention.StdCall,
        CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcBindingSetAuthInfo2(IntPtr Binding, String ServerPrincName,
                                                          RpcProtectionLevel AuthnLevel, RpcAuthentication AuthnSvc,
                                                          IntPtr p, uint AuthzSvc);

    [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingSetAuthInfoW", CallingConvention = CallingConvention.StdCall,
        CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RpcError RpcBindingSetAuthInfo(IntPtr Binding, String ServerPrincName,
                                                         RpcProtectionLevel AuthnLevel, RpcAuthentication AuthnSvc,
                                                         [In] ref SEC_WINNT_AUTH_IDENTITY AuthIdentity,
                                                         uint AuthzSvc);

    //[DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingFree", CallingConvention = CallingConvention.StdCall,
    //    CharSet = CharSet.Unicode, SetLastError = true)]
    //public static extern RpcError RpcBindingFree(ref IntPtr lpString);
  }
}