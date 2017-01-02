﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class AttributeType : IComparable {
    public string Name { get; private set; }
    public string ShortName { get; private set; }
    public string PrimaryDescription { get; private set; }
    public string SecondaryDescription { get; private set; }
    public string ShortDescription { get; private set; }
    public Color Color { get; private set; }
    public bool IsAssignable { get; private set; }
    int order;

    private AttributeType(
        string name,
        string shortName,
        string primaryDescription,
        string secondaryDescription,
        string shortDescription,
        Color color,
        int order, bool
        isAssignable = true) {

        this.Name = Util.Color(name, color);
        this.ShortName = Util.Color(shortName, color);
        this.PrimaryDescription = primaryDescription;
        this.SecondaryDescription = secondaryDescription;
        this.ShortDescription = shortDescription;
        this.Color = color;
        this.order = order;
        this.IsAssignable = isAssignable;
        ALL.Add(this);
    }

    public static readonly IList<AttributeType> ALL = new List<AttributeType>();

    public static readonly AttributeType LEVEL = new AttributeType("Level",
                                                                      "Lvl",
                                                                      "Current level of power.",
                                                                      "When leveling up, you can assign points and select perks.",
                                                                      "Current level.",
                                                                      Color.white,
                                                                      -1,
                                                                      false);

    public static readonly AttributeType STRENGTH = new AttributeType("Strength",
                                                                      "STR",
                                                                      "Increases basic attack damage.",
                                                                      "Increases health.",
                                                                      "Increases basic attack damage.",
                                                                      Color.red,
                                                                      0);

    public static readonly AttributeType INTELLIGENCE = new AttributeType("Intelligence",
                                                                          "INT",
                                                                          "Increases spell effects.",
                                                                          "Increases critical hit rate.",
                                                                          "Increases spell effects.",
                                                                          Color.blue,
                                                                          1);

    public static readonly AttributeType AGILITY = new AttributeType("Agility",
                                                                       "AGI",
                                                                       "Increases Charge generation.",
                                                                       "Increases critical hit rate and accuracy.",
                                                                       "Increases Charge generation.",
                                                                       Color.green,
                                                                       2);

    public static readonly AttributeType VITALITY = new AttributeType("Vitality",
                                                                      "VIT",
                                                                      "Increases <color=lime>Life</color>.",
                                                                      "Increases armor and magical resistances.",
                                                                      "Increases Life.",
                                                                      Color.yellow,
                                                                      3);

    public static readonly AttributeType[] COMBAT_ATTRIBUTES = new AttributeType[] { STRENGTH, INTELLIGENCE, AGILITY, VITALITY };
    public static readonly AttributeType[] RESTORED_ATTRIBUTES = new AttributeType[] { STRENGTH, INTELLIGENCE, AGILITY, VITALITY };

    public int CompareTo(object obj) {
        AttributeType other = (AttributeType)obj;
        int res = this.order.CompareTo(other.order);
        if (res == 0) {
            res = this.Name.CompareTo(other.Name);
        }
        return res;
    }

    public override bool Equals(object obj) {
        // Check for null values and compare run-time types.
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }
        AttributeType at = (AttributeType)obj;
        return this.Name.Equals(at.Name);
    }

    public override int GetHashCode() {
        return this.Name.GetHashCode();
    }

    public static string SplatDisplay(int i) {
        return i.ToString("+#;-#");
    }

    public static Color DetermineColor(AttributeType attributeType, int amount) {
        Color textColor = Color.clear;
        if (amount < 0) {
            textColor = attributeType.Color;
        } else if (amount == 0) {
            textColor = Color.grey;
        } else {
            textColor = attributeType.Color;
        }
        return textColor;
    }
}