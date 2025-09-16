// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.SquareEquationSolver
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.Common;

/// <summary>A square equation solver.</summary>
public static class SquareEquationSolver
{
  /// <summary>
  /// Calculate square root for <see cref="T:System.Decimal" /> number <paramref name="a" />. The result of the calculations will differ from an actual value of the root on less than epslion.
  /// More details about algorithm are here: https://stackoverflow.com/questions/4124189/performing-math-operations-on-decimal-datatype-in-c
  /// and here: https://doc.lagout.org/security/Hackers%20Delight.pdf#%5B%7B%22num%22%3A1017%2C%22gen%22%3A0%7D%2C%7B%22name%22%3A%22XYZ%22%7D%2C5%2C738%2Cnull%5D
  /// </summary>
  /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown when <paramref name="a" /> or <paramref name="epsilon" /> is less than zero.</exception>
  /// <param name="a">The number, from which we need to calculate the square root.</param>
  /// <param name="epsilon">(Optional) The accuracy of calculation of the root from our number.</param>
  /// <returns />
  public static Decimal Sqrt(this Decimal a, Decimal epsilon = 0.0M)
  {
    if (a < 0M)
      throw new ArgumentOutOfRangeException("Cannot calculate square root from a negative number");
    if (epsilon < 0M)
      throw new ArgumentOutOfRangeException("epsilon can't be a negative number");
    Decimal num1 = (Decimal) Math.Sqrt((double) a);
    Decimal num2;
    do
    {
      num2 = num1;
      if (num2 == 0.0M)
        return 0M;
      num1 = (num2 + a / num2) / 2M;
    }
    while (Math.Abs(num2 - num1) > epsilon);
    return num1;
  }

  /// <summary>
  /// Solve quadratic equation a*X^2 + b*x + c = 0. Returns <c>null</c> if there is no solution for the equation.
  /// All calculations done with <see cref="T:System.Double" />.
  /// </summary>
  /// <param name="a">Coefficient a.</param>
  /// <param name="b">Coefficient b.</param>
  /// <param name="c">Coefficient c.</param>
  /// <returns />
  public static (double X1, double X2)? SolveQuadraticEquation(double a, double b, double c)
  {
    double d = b * b - 4.0 * a * c;
    if (d > 0.0)
    {
      double num = Math.Sqrt(d);
      return new (double, double)?(((-b - num) / (2.0 * a), (-b + num) / (2.0 * a)));
    }
    double num1;
    return d == 0.0 ? new (double, double)?((num1 = -b / (2.0 * a), num1)) : new (double, double)?();
  }

  /// <summary>
  /// Solve quadratic equation a*X^2 + b*x + c = 0. Returns <c>null</c> if there is no solution for the equation.
  /// All calculations done with <see cref="T:System.Decimal" />.
  /// </summary>
  /// <param name="a">Coefficient a.</param>
  /// <param name="b">Coefficient b.</param>
  /// <param name="c">Coefficient c.</param>
  /// <returns />
  public static (Decimal X1, Decimal X2)? SolveQuadraticEquation(Decimal a, Decimal b, Decimal c)
  {
    Decimal a1 = b * b - 4M * a * c;
    if (a1 > 0M)
    {
      Decimal num = a1.Sqrt();
      return new (Decimal, Decimal)?(((-b - num) / (2M * a), (-b + num) / (2M * a)));
    }
    Decimal num1;
    return a1 == 0M ? new (Decimal, Decimal)?((num1 = -b / (2M * a), num1)) : new (Decimal, Decimal)?();
  }
}
