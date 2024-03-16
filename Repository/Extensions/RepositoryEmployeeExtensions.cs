using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryEmployeeExtensions
    {
        public static IQueryable<Employee> FilterEmployees(this IQueryable<Employee> employees, uint? minAge, uint? maxAge)
        {
            // Check if both minAge and maxAge are null
            if (!minAge.HasValue && !maxAge.HasValue)
            {
                // If both are null, return all employees without filtering
                return employees;
            }

            // Apply age filter based on the presence of minAge and maxAge
            if (minAge.HasValue && maxAge.HasValue && minAge < maxAge)
            {
                // Both minAge and maxAge have values, filter employees within the age range
                return employees.Where(e => e.Age >= minAge.Value && e.Age <= maxAge.Value);
            }
            else if (minAge.HasValue)
            {
                // Only minAge has value, filter employees with age greater than or equal to minAge
                return employees.Where(e => e.Age >= minAge.Value);
            }
            else
            {
                // Only maxAge has value, filter employees with age less than or equal to maxAge
                return employees.Where(e => e.Age <= maxAge.Value);
            }
        }
        public static IQueryable<Employee> PageEmployees(this IQueryable<Employee> employees, int? pageNumber, int? pageSize)
        {
            // Check if pageNumber or pageSize is null
            if (!pageNumber.HasValue || !pageSize.HasValue)
            {
                // Return all employees without pagination
                return employees;
            }
            // Apply pagination
            int skipCount = (pageNumber.Value - 1) * pageSize.Value;
            return employees.Skip(skipCount)
                            .Take(pageSize.Value);
        }
        public static IQueryable<Employee> SearchEmployees(this IQueryable<Employee> employees, string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return employees;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return employees.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }
    }
}
