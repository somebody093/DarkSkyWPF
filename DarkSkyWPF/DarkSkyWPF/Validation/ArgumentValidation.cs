using System;

namespace DarkSkyWPF.Validation
{
  internal static class ArgumentValidation
  {
    /// <summary>
    /// Throws an ArgimentNullException when the given argument's value is null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">The value of the argument</param>
    /// <param name="name">The name of the argument</param>
    /// <returns>Returns the argument value in case it was not null.</returns>
    public static T ThrowIfNull<T>(T value, string name)
    {
      if (value == null)
      {
        throw new ArgumentNullException(name);
      }

      return value;
    }
  }
}
