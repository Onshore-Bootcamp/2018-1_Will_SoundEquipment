using SoundEquipmentBLL.ErrorLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoundEquipmentBLL
{
    public class ClassForBLLMethod
    {
        public ClassForBLLMethod(string connectionString)
        {
            _connectionString = connectionString;
        }
        private readonly string _connectionString;

        //Method to get the most frequent manufacturer in the products table
        public List<string> GetMostFrequentManufacturer(List<string> manufacturerList)
        {
            List<string> mostCommon = null;

            try
            {
                mostCommon = manufacturerList.GroupBy(manufacturer => manufacturer)
                    .OrderByDescending(manufacturer => manufacturer.Count())
                    .Select(x => x.Key)
                .Take(2)
                .ToList();

                //Rather than using this:
                //.First().Key;

                //Don't have to use .Select because the return type is the same as the type of argument:
                //.Select(x=>x.Key);
            }

            catch (Exception exception)
            {
                ErrorLogger.LogExceptions(exception);
            }

            finally { }

            //return a list
            return mostCommon;
        }
    }
}
