using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class IdleNumber {
  public double value;
  public int exp;

  public Dictionary<int, string> names;

  public IdleNumber(double val) {
    if (val < 1000.0) {
      this.value = val;
      this.exp = 0;
    } else {
      int e = (int) Math.Floor(Math.Log10(Math.Abs(val)) / 3) * 3;
      double num = val / (Math.Pow(10, e));

      this.value = num;
      this.exp = e;
    }

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

  public void add(IdleNumber num) {
    Debug.Log("ADD: " + num.value + " " + num.exp);
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
      Debug.Log("ELSE");
      Debug.Log("ACTUAL: " + this.value + " " + this.exp);
      double factor = Math.Pow(10, num.exp - this.exp);
      this.value /= factor;
      this.value += (num.value * Math.Pow(10, num.exp));
      if (this.value >= 1000.0) {
        Debug.Log("MAIOR Q 1000");
        this.exp = num.exp;
        this.value /= 1000.0;
      }
    }
  }

  public void sub(double cost) {
    IdleNumber num = new IdleNumber(cost);
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
    Debug.Log("SUB: " + this.value + " " + this.exp);
    num = null;
  }

  public bool canBuy(double cost) {
    IdleNumber temp = new IdleNumber(this.value * Math.Pow(10, this.exp));
    IdleNumber num = new IdleNumber(cost);
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
