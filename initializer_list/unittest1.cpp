#include "stdafx.h"
#include "CppUnitTest.h"

#include <vector>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace initializer_list
{		
	TEST_CLASS(UnitTest1)
	{
    Logger logger;

	public:
		
		TEST_METHOD(testInitializerList)
		{
      std::vector<int> values;
//      std::vector<int> values{ 1, 2, 3, 4}; // no work in msvc11
      values.push_back(1);
      values.push_back(2);
      values.push_back(3);
      values.push_back(4);

      for (auto elm: values) {
         logger.WriteMessage(ToString(elm).c_str());
      }
		}

		TEST_METHOD(testInitializerList2)
    {
      int values[] = {1, 2, 3, 4};
      for (auto elm: values) {
         logger.WriteMessage(ToString(elm).c_str());
      }
    }

	};
}