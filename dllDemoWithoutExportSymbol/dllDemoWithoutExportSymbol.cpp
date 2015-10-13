// dllDemoWithoutExportSymbol.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"

#include <string>
#include <xutility>

#include <string.h>

#include "dllDemoWithoutExportSymbol.h"

DLLDEMOWITHOUTEXPORTSYMBOL_API
int fun(const wchar_t* szFun) {
  if (nullptr == szFun) {
    return 0;
  }
  return (int) wcsnlen_s(szFun, 100);
}

class Klass::KlassImpl {
  std::wstring value;

public:
  KlassImpl() {}

  KlassImpl(const wchar_t *value) {
    if (nullptr == value)
      return;
    this->value = std::move(std::wstring(value));
  }

  int method() const {
    return value.size();
  }
};

Klass::Klass() {
  impl = new KlassImpl();
}

Klass::Klass(const wchar_t * value) {
  impl = new KlassImpl(value);
}

int Klass::method() const {
  return impl->method();
}