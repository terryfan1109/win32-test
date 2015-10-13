// dllDemo.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "dllDemo.h"


// This is an example of an exported variable
DLLDEMO_API int ndllDemo=0;

// This is an example of an exported function.
DLLDEMO_API int fndllDemo(void)
{
	return 42;
}

// This is the constructor of a class that has been exported.
// see dllDemo.h for the class definition
CdllDemo::CdllDemo()
{
	return;
}
