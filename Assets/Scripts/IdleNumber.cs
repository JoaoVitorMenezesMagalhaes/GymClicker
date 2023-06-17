using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class IdleNumber {
  public double value;
  public int exp;

  public Dictionary<int, string> names;

  public IdleNumber(double val, int exp) {
    this.value = val;
    this.exp = exp;

    this.names = new Dictionary<int, string>();

    this.names.Add(6, "Million");
    this.names.Add(9, "Billion");
    this.names.Add(12, "Trillion");
    this.names.Add(15, "Quadrillion");
    this.names.Add(18, "Quintillion");
    this.names.Add(21, "Sextillion");
    this.names.Add(24, "Septillion");
    this.names.Add(27, "Octillion");
    this.names.Add(30, "Nonillion");
    this.names.Add(33, "Decillion");
  }

  public void add(double val, int exp) {
    IdleNumber num = new IdleNumber(val, exp);
    if (this.exp == num.exp) {
      this.value += num.value;
      if (this.value >= 1000.0) {
        this.exp += 3;
        this.value /= 1000.0;
      }
    } else if (this.exp > num.exp) {
      double factor = Math.Pow(10, this.exp - num.exp);
      num.value /= factor;
      this.value += num.value;
      if (this.value >= 1000.0) {
        this.exp += 3;
        this.value /= 1000.0;
      }
    } else {
      double factor = Math.Pow(10, num.exp - this.exp);
      this.value /= factor;
      this.value += (num.value * Math.Pow(10, num.exp));
      if (this.value >= 1000.0) {
        this.exp = num.exp;
        this.value /= 1000.0;
      }
    }
  }

  public void sub(double cost, int exp) {
    IdleNumber num = new IdleNumber(cost, exp);
    if (this.exp == num.exp) {
      this.value -= num.value;
      if (this.value < 1.0) {
        this.value = Math.Round(this.value * 1000.0, 2);
        this.exp -= 3;
      }
    } else if (this.exp > num.exp) {
      double factor = Math.Pow(10, this.exp - num.exp);
      num.value = Math.Round(num.value / factor, 2);
      this.value -= num.value;
      if (this.value < 1.0) {
        this.value = Math.Round(this.value * 1000.0, 2);
        this.exp -= 3;
      }
    } else {
      double factor = Math.Pow(10, num.exp - this.exp);
      this.value = Math.Round(this.value / factor, 2);
      this.value -= num.value;
      if (this.value < 1.0) {
        this.value = Math.Round(this.value * 1000.0, 2);
        this.exp = num.exp - 3;
      }
    }
    num = null;
  }

  public void mult(IdleNumber factor) {
    this.value *= factor.value;
    this.exp += factor.exp;

    if (this.value >= 1000.0) {
      int additionalExp = (int) Math.Floor(Math.Log10(Math.Abs(this.value)) / 3) * 3;
      double num = this.value / (Math.Pow(10, additionalExp));

      this.value = num;
      this.exp += additionalExp;
    }
 }

  public void div(IdleNumber divisor) {
    this.value /= divisor.value;
    this.exp -= divisor.exp;

    if (this.value < 1.0) {
      int reductionExp = (int) Math.Floor(Math.Log10(Math.Abs(this.value)) / 3) * 3;
      double num = this.value * (Math.Pow(10, reductionExp));

      this.value = num;
      this.exp -= reductionExp;
    }
  }

  public bool canBuy(double cost, int exp) {
    IdleNumber temp = new IdleNumber(this.value, this.exp);
    IdleNumber num = new IdleNumber(cost, exp);
    if (temp.exp == num.exp) {
      temp.value -= num.value;
      if (temp.value < 1.0) {
        temp.value *= 1000.0;
        temp.exp -= 3;
      }
    } else if (temp.exp > num.exp) {
      double factor = Math.Pow(10, temp.exp - num.exp);
      num.value /= factor;
      temp.value -= num.value;
      if (temp.value < 1.0) {
        temp.value *= 1000.0;
        temp.exp -= 3;
      }
    } else {
      double factor = Math.Pow(10, num.exp - temp.exp);
      temp.value /= factor;
      temp.value -= num.value;
      if (temp.value < 1.0) {
        temp.value *= 1000.0;
        temp.exp = num.exp - 3;
      }
    }

    bool canSubtract = (temp.value >= 0 && temp.exp >= 0);
    temp = null;
    num = null;
    return canSubtract;
  }

  public string formatNumber() {
    if (this.exp < 6) {
      return String.Format("{0}", Math.Floor(this.value * Math.Pow(10, this.exp)));
    } else {
      return String.Format("{0} {1}", Math.Round(this.value, 2), this.names[this.exp]);
    }
  }
}
