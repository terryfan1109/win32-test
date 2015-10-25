// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the DLLDEMO_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// DLLDEMO_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef DLLDEMO_EXPORTS
#define DLLDEMO_API __declspec(dllexport)
#else
#define DLLDEMO_API __declspec(dllimport)
#endif

// This class is exported from the dllDemo.dll
class DLLDEMO_API CdllDemo {
public:

	CdllDemo(void);
	// TODO: add your methods here.
};

extern DLLDEMO_API int ndllDemo;
  
DLLDEMO_API int fndllDemo(void);
