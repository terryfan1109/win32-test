#include "stdafx.h"
#include "CppUnitTest.h"

#include <algorithm>
#include <string>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace lamda_001
{		
  Logger logger;

  TEST_CLASS(UnitTest1)
	{
	public:
		
		TEST_METHOD(testLamda)
		{
      int values[] = {-1, 3, 2, 7, 100, 23};

      auto it = std::find_if(std::begin(values), std::end(values), [](int elm) { return elm == 7;});
      Assert::AreEqual(*it, 7);
		}

		TEST_METHOD(testLamda2)
		{
      int values[] = {-1, 3, 2, 7, 100, 23};

      auto it = std::find_if(std::begin(values), std::end(values), [](int elm) { return elm == 101;});
      Assert::AreEqual(it, std::end(values));
		}

    TEST_METHOD(testLamda3)
		{
      int values[] = {-1, 3, 2, 7, 100, 23};
      int target = 2;

      auto it = std::find_if(std::begin(values), std::end(values), [&target](int elm) { return elm == target;});
      Assert::AreEqual(*it, target);
		}

    TEST_METHOD(testLamda4)
		{
      auto &_logger = logger;
      int values[] = {-1, 3, 2, 7, 100, 23};
      std::for_each(std::begin(values), std::end(values), [&_logger](int elm) {_logger.WriteMessage(ToString(elm).c_str());}); 
		}

    TEST_METHOD(testLamda5)
		{
      int values[] = {-1, 3, 2, 7, 100, 23};
      std::sort(std::begin(values), std::end(values), [](int rhs, int lhs) {
        return rhs < lhs;
      }); 

      auto &_logger = logger;
      std::for_each(std::begin(values), std::end(values), [&_logger](int elm) {_logger.WriteMessage(ToString(elm).c_str());}); 
    }  
  };
}