#pragma once

#ifdef DLLDEMOWITHOUTEXPORTSYMBOL_EXPORTS
#define DLLDEMOWITHOUTEXPORTSYMBOL_API __declspec(dllexport)
#else
#define DLLDEMOWITHOUTEXPORTSYMBOL_API __declspec(dllimport)
#endif

DLLDEMOWITHOUTEXPORTSYMBOL_API int fun(const wchar_t* szFun);

class DLLDEMOWITHOUTEXPORTSYMBOL_API Klass
{
private:
  class KlassImpl;
  KlassImpl *impl;

public:
  Klass();
  Klass(const wchar_t *);

  int method() const;
};