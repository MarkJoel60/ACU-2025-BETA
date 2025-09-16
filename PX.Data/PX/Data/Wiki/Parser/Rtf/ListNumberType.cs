// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.ListNumberType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public enum ListNumberType
{
  Arabic = 0,
  RomanUpperCase = 1,
  RomanLowerCase = 2,
  LetterUpperCase = 3,
  LetterLowerCase = 4,
  OrdinalNum = 5,
  CardinalTextNum = 6,
  OrdinalTextNum = 7,
  Kanji = 10, // 0x0000000A
  KanjiDigit = 11, // 0x0000000B
  KatakanaAIUEO = 12, // 0x0000000C
  KatakanaIROHA = 13, // 0x0000000D
  DoubleByteChar = 14, // 0x0000000E
  SingleByteChar = 15, // 0x0000000F
  KanjiNum3 = 16, // 0x00000010
  KanjiNum4 = 17, // 0x00000011
  CircleNum = 18, // 0x00000012
  DoubleByteArabic = 19, // 0x00000013
  KatakanaAIUEODouble = 20, // 0x00000014
  KatakanaIROHADouble = 21, // 0x00000015
  ArabicZero = 22, // 0x00000016
  Bullet = 23, // 0x00000017
  KoreanGANADA = 24, // 0x00000018
  KoreanCHOSUNG = 25, // 0x00000019
  Chinese1 = 26, // 0x0000001A
  Chinese2 = 27, // 0x0000001B
  Chinese3 = 28, // 0x0000001C
  Chinese4 = 29, // 0x0000001D
  ChineseZodiac1 = 30, // 0x0000001E
  ChineseZodiac2 = 31, // 0x0000001F
  ChineseZodiac3 = 32, // 0x00000020
  TaiwanDb1 = 33, // 0x00000021
  TaiwanDb2 = 34, // 0x00000022
  TaiwanDb3 = 35, // 0x00000023
  TaiwanDb4 = 36, // 0x00000024
  ChineseDb1 = 37, // 0x00000025
  ChineseDb2 = 38, // 0x00000026
  ChineseDb3 = 39, // 0x00000027
  ChineseDb4 = 40, // 0x00000028
  KoreanDb1 = 41, // 0x00000029
  KoreanDb2 = 42, // 0x0000002A
  KoreanDb3 = 43, // 0x0000002B
  KoreanDb4 = 44, // 0x0000002C
  Hebrew = 45, // 0x0000002D
  ArabicAlifBaTah = 46, // 0x0000002E
  HebrewBiblical = 47, // 0x0000002F
  ArabicAbjad = 48, // 0x00000030
  NoNumber = 255, // 0x000000FF
}
