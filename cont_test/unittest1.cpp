#include "stdafx.h"
#include "CppUnitTest.h"

#include <string>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using std::wstring;

class MyType {
  static Logger logger;

public:
  MyType() {
    MyType::logger.WriteMessage((wstring(L"cotr") + ToString((int) this)).c_str());
  };

  MyType(const MyType& rhs) {
    MyType::logger.WriteMessage((wstring(L"copy cotr") + ToString((int) this)).c_str());
    value = rhs.value;
  }
  MyType(MyType&& rhs) noexcept {
    using std::swap;
    MyType::logger.WriteMessage((wstring(L"move cotr") + ToString((int) this)).c_str());
    swap(value, rhs.value);
  }
  MyType& operator=(const MyType& rhs) {
    MyType::logger.WriteMessage((wstring(L"copy assignment") + ToString((int) this)).c_str());
    value = rhs.value;
    return *this;
  }
  MyType& operator=(MyType&& rhs) {
    using std::swap;
    MyType::logger.WriteMessage((wstring(L"move assignment") + ToString((int) this)).c_str());
    swap(value, rhs.value);
    return *this;
  }
  ~MyType() _NOEXCEPT {
    MyType::logger.WriteMessage((wstring(L"decotr") + ToString((int) this)).c_str());
  };

private:
  std::wstring value;
};

class MyType2 {
  static Logger logger;

public:
  MyType2() {
    MyType2::logger.WriteMessage((wstring(L"cotr") + ToString((int) this)).c_str());
  }

//  template<typename T1>
//  MyType2(T1 &&value): value(std::forward<T1>(value)) {
//    MyType2::logger.WriteMessage((wstring(L"customized cotr") + ToString((int) this)).c_str());
//  }

  MyType2(std::wstring &&value): value(value) {
    MyType2::logger.WriteMessage((wstring(L"customized cotr by move") + ToString((int) this)).c_str());
  }

  MyType2(const std::wstring &value): value(value) {
    MyType2::logger.WriteMessage((wstring(L"customized cotr by copy") + ToString((int) this)).c_str());
  }

  MyType2(const MyType2& rhs) {
    MyType2::logger.WriteMessage((wstring(L"copy cotr") + ToString((int) this)).c_str());
    value = rhs.value;
  }
  MyType2(MyType2&& rhs) {
    using std::swap;
    MyType2::logger.WriteMessage((wstring(L"move cotr") + ToString((int) this)).c_str());
    swap(value, rhs.value);
  }
  MyType2& operator=(const MyType2& rhs) {
    MyType2::logger.WriteMessage((wstring(L"copy assignment") + ToString((int) this)).c_str());
    value = rhs.value;
    return *this;
  }
  MyType2& operator=(MyType2&& rhs) {
    using std::swap;
    MyType2::logger.WriteMessage((wstring(L"move assignment") + ToString((int) this)).c_str());
    swap(value, rhs.value);
    return *this;
  }
  ~MyType2() {
    MyType2::logger.WriteMessage((wstring(L"decotr") + ToString((int) this)).c_str());
  };

private:
  std::wstring value;
};

static MyType f() {
  MyType value;
  return value;
}

static std::wstring str() {
  return std::wstring(L"abc");
}

static std::wstring globalValue(L"xyz");
static const std::wstring& str2() {
  return globalValue;
}

namespace cont_test
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		
		TEST_METHOD(testMoveCotr)
		{
      auto result = f();
		}

		TEST_METHOD(testMoveCotr2)
		{
      MyType result;
      result = f();
		}

    TEST_METHOD(testMoveCotr3)
    {
      MyType2 result(str());
    }

    TEST_METHOD(testMoveCotr4)
    {
      MyType2 result(str2());
    }

	};
}