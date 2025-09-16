// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.DataSync.BAISchema
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA.BankFeed.DataSync;

/// <summary>Helper classes for parsing BAI2 files</summary>
public static class BAISchema
{
  public const string BaiFields = "BAI Fields";
  public const string RecordCode = "Record Code";
  public const string CustomerAccountNumber = "Customer Account Number";
  public const string BankReferenceNumber = "Bank Reference Number";
  public const string AsOfDate = "As-of-date";
  public const string Amount = "Amount";
  public const string TranDesc = "Tran Desc";
  public const string CustomerReferenceNumber = "Customer Reference Number";
  public const string TypeCode = "Type Code";
  public const string AdditionalTranDesc = "Additional Tran Desc";
  public const string FileIdentificationNumber = "File Identification Number";
  public const string FileCreationDate = "File Creation Date";
  public const string AccountCurrencyCode = "Account Currency Code";
  public const string TypeCodeDesc = "Type Code Description";
  public static Dictionary<int, (string Name, Type FieldType)> Schema = new Dictionary<int, (string, Type)>()
  {
    {
      0,
      ("Customer Account Number", typeof (string))
    },
    {
      1,
      ("Bank Reference Number", typeof (string))
    },
    {
      2,
      ("As-of-date", typeof (DateTime))
    },
    {
      3,
      (nameof (Amount), typeof (Decimal))
    },
    {
      4,
      ("Tran Desc", typeof (string))
    },
    {
      5,
      ("Customer Reference Number", typeof (string))
    },
    {
      6,
      ("Type Code", typeof (string))
    },
    {
      7,
      ("File Identification Number", typeof (string))
    },
    {
      8,
      ("Account Currency Code", typeof (string))
    },
    {
      9,
      ("File Creation Date", typeof (DateTime))
    },
    {
      10,
      ("Additional Tran Desc", typeof (string))
    },
    {
      11,
      ("Type Code Description", typeof (string))
    }
  };
  public static Dictionary<int, BAICode> BAICodes = new Dictionary<int, BAICode>()
  {
    {
      10,
      new BAICode(10, BAICodeType.Skip, "Opening Ledger")
    },
    {
      11,
      new BAICode(11, BAICodeType.Skip, "AVG OPENING LEDGER BAL MTD")
    },
    {
      12,
      new BAICode(12, BAICodeType.Skip, "AVG OPENING LEDGER BAL YTD")
    },
    {
      15,
      new BAICode(15, BAICodeType.Skip, "Closing Ledger")
    },
    {
      20,
      new BAICode(20, BAICodeType.Skip, "Average Closing Ledger MTD")
    },
    {
      21,
      new BAICode(21, BAICodeType.Skip, "AVG LEDGER BALANCE PREVIOUS MONTH")
    },
    {
      22,
      new BAICode(22, BAICodeType.Skip, "AGGREGATE BALANCE ADJUSTMENTS")
    },
    {
      24,
      new BAICode(24, BAICodeType.Skip, "AVERAGE CLOSING LEDGER - PREVIOUS MONTH")
    },
    {
      25,
      new BAICode(25, BAICodeType.Skip, "Average Closing Ledger YTD")
    },
    {
      30,
      new BAICode(30, BAICodeType.Skip, "Current Ledger")
    },
    {
      35,
      new BAICode(35, BAICodeType.Skip, "Opening Available")
    },
    {
      37,
      new BAICode(37, BAICodeType.Skip, "ACH NET POSITION")
    },
    {
      39,
      new BAICode(39, BAICodeType.Skip, "OPEN AVAILABLE & TOTAL SD ACH DTC DEP")
    },
    {
      40,
      new BAICode(40, BAICodeType.Skip, "Opening Available Next Business Day")
    },
    {
      41,
      new BAICode(41, BAICodeType.Skip, "AVG OPENING AVAILABLE BAL MTD")
    },
    {
      42,
      new BAICode(42, BAICodeType.Skip, "AVG OPENING AVAILABLE BAL YTD")
    },
    {
      43,
      new BAICode(43, BAICodeType.Skip, "AVG CLOSING AVL BAL PREV MNTH")
    },
    {
      44,
      new BAICode(44, BAICodeType.Skip, "DISBURSING OPENING AVAILABLE BALANCE")
    },
    {
      45,
      new BAICode(45, BAICodeType.Skip, "Closing Available")
    },
    {
      50,
      new BAICode(50, BAICodeType.Skip, "Average Closing Available MTD")
    },
    {
      51,
      new BAICode(51, BAICodeType.Skip, "AVERAGE CLOSING AVAILABLE - LAST MONTH")
    },
    {
      54,
      new BAICode(54, BAICodeType.Skip, "AVG CLOSING AVAILABLE - YTD MONTH")
    },
    {
      55,
      new BAICode(55, BAICodeType.Skip, "Average Closing Available YTD")
    },
    {
      56,
      new BAICode(56, BAICodeType.Skip, "LOAN BALANCE")
    },
    {
      57,
      new BAICode(57, BAICodeType.Skip, "Investment Sweep Position")
    },
    {
      59,
      new BAICode(59, BAICodeType.Skip, "Current Available")
    },
    {
      60,
      new BAICode(60, BAICodeType.Skip, "Current Available")
    },
    {
      61,
      new BAICode(61, BAICodeType.Skip, "AVG CURRENT AVAILABLE BAL YTD")
    },
    {
      62,
      new BAICode(62, BAICodeType.Skip, "TOTAL FLOAT")
    },
    {
      63 /*0x3F*/,
      new BAICode(63 /*0x3F*/, BAICodeType.Skip, "TARGET BALANCE")
    },
    {
      65,
      new BAICode(65, BAICodeType.Skip, "ADJUSTED BALANCES")
    },
    {
      66,
      new BAICode(66, BAICodeType.Skip, "ADJUSTED BALANCE MTD")
    },
    {
      67,
      new BAICode(67, BAICodeType.Skip, "ADJUSTED BALANCE YTD")
    },
    {
      68,
      new BAICode(68, BAICodeType.Skip, "0 Day Available")
    },
    {
      70,
      new BAICode(70, BAICodeType.Skip, "0 Day Available")
    },
    {
      72,
      new BAICode(72, BAICodeType.Skip, "1 Day Available")
    },
    {
      73,
      new BAICode(73, BAICodeType.Skip, "Availability Adjustment")
    },
    {
      74,
      new BAICode(74, BAICodeType.Skip, "2 or More Days Available")
    },
    {
      75,
      new BAICode(75, BAICodeType.Skip, "CLOSING BALANCE - 3+ DAYS FLT")
    },
    {
      76,
      new BAICode(76, BAICodeType.Skip, "Balance Adjustment")
    },
    {
      77,
      new BAICode(77, BAICodeType.Skip, "2 Day Available")
    },
    {
      78,
      new BAICode(78, BAICodeType.Skip, "3 Day Available")
    },
    {
      79,
      new BAICode(79, BAICodeType.Skip, "4 Day Available")
    },
    {
      80 /*0x50*/,
      new BAICode(80 /*0x50*/, BAICodeType.Skip, "5 Day Available")
    },
    {
      81,
      new BAICode(81, BAICodeType.Skip, "6 or More Days Available")
    },
    {
      82,
      new BAICode(82, BAICodeType.Skip, "AVERAGE ONE DAY FLOAT MTD")
    },
    {
      83,
      new BAICode(83, BAICodeType.Skip, "AVERAGE ONE DAY FLOAT YTD")
    },
    {
      84,
      new BAICode(84, BAICodeType.Skip, "AVERAGE TWO DAY FLOAT MTD")
    },
    {
      85,
      new BAICode(85, BAICodeType.Skip, "AVERAGE TWO DAY FLOAT YTD")
    },
    {
      86,
      new BAICode(86, BAICodeType.Skip, "TRANSFER CALCULATION")
    },
    {
      100,
      new BAICode(100, BAICodeType.Skip, "Total Credits")
    },
    {
      101,
      new BAICode(101, BAICodeType.Skip, "MTD TOTAL CREDIT AMOUNT")
    },
    {
      105,
      new BAICode(105, BAICodeType.Skip, "CREDITS NOT DETAILED")
    },
    {
      106,
      new BAICode(106, BAICodeType.Skip, "DEPOSITS SUBJECT TO FLOAT")
    },
    {
      107,
      new BAICode(107, BAICodeType.Skip, "TOTAL ADJUSTMENT CREDITS YTD")
    },
    {
      108,
      new BAICode(108, BAICodeType.Credit, "CREDIT (ANY TYPE)")
    },
    {
      109,
      new BAICode(109, BAICodeType.Skip, "CURRENT DAY TOTAL LOCKBOX DEP.")
    },
    {
      110,
      new BAICode(110, BAICodeType.Skip, "Total Lockbox Deposits")
    },
    {
      115,
      new BAICode(115, BAICodeType.Credit, "Lockbox Deposit")
    },
    {
      116,
      new BAICode(116, BAICodeType.Credit, "ITEM IN LOCKBOX DEPOSIT")
    },
    {
      118,
      new BAICode(118, BAICodeType.Credit, "LOCKBOX ADJUSTMENT CREDIT")
    },
    {
      120,
      new BAICode(120, BAICodeType.Skip, "EDI TRANSACTION CREDITS")
    },
    {
      121,
      new BAICode(121, BAICodeType.Credit, "EDI TRANSACTION CREDIT")
    },
    {
      122,
      new BAICode(122, BAICodeType.Credit, "EDIBANX CREDIT RECEIVED")
    },
    {
      123,
      new BAICode(123, BAICodeType.Credit, "EDIBANX CREDIT RETURNED")
    },
    {
      130,
      new BAICode(130, BAICodeType.Skip, "TOTAL CONCENTRATION CREDITS")
    },
    {
      131,
      new BAICode(131, BAICodeType.Skip, "TOTAL DTC CREDITS")
    },
    {
      135,
      new BAICode(135, BAICodeType.Credit, "DTC CONCENTRATION CREDIT")
    },
    {
      136,
      new BAICode(136, BAICodeType.Credit, "ITEM IN DTC DEPOSIT")
    },
    {
      138,
      new BAICode(138, BAICodeType.Skip, "Total SEPA Credits")
    },
    {
      139,
      new BAICode(139, BAICodeType.Credit, "SEPA Return Credit")
    },
    {
      140,
      new BAICode(140, BAICodeType.Skip, "Total ACH Credits")
    },
    {
      141,
      new BAICode(141, BAICodeType.Credit, "SEPA Credit Transfer")
    },
    {
      142,
      new BAICode(142, BAICodeType.Credit, "ACH CREDIT RECEIVED")
    },
    {
      143,
      new BAICode(143, BAICodeType.Credit, "ITEM IN ACH DEPOSIT")
    },
    {
      144 /*0x90*/,
      new BAICode(144 /*0x90*/, BAICodeType.Credit, "SEPA Adjustment Credit")
    },
    {
      145,
      new BAICode(145, BAICodeType.Credit, "ACH CONCENTRATION CREDIT")
    },
    {
      146,
      new BAICode(146, BAICodeType.Skip, "TOTAL BANK CARD DEPOSITS")
    },
    {
      147,
      new BAICode(147, BAICodeType.Credit, "INDIVIDUAL BANK CARD DEPOSIT")
    },
    {
      150,
      new BAICode(150, BAICodeType.Skip, "TOTAL PREAUTH PAYMENT DEPOSIT")
    },
    {
      155,
      new BAICode(155, BAICodeType.Credit, "INDIV PREAUTH DRAFT CREDIT")
    },
    {
      156,
      new BAICode(156, BAICodeType.Credit, "ITEM IN PAC DEPOSIT")
    },
    {
      158,
      new BAICode(158, BAICodeType.Credit, "Real Time Payment")
    },
    {
      159,
      new BAICode(159, BAICodeType.Skip, "Total Real Time Payment Credits")
    },
    {
      160 /*0xA0*/,
      new BAICode(160 /*0xA0*/, BAICodeType.Skip, "TOTAL ACH DISB FUNDING CREDITS")
    },
    {
      162,
      new BAICode(162, BAICodeType.Skip, "CTP SETTLEMENT")
    },
    {
      163,
      new BAICode(163, BAICodeType.Skip, "CTP CREDITS")
    },
    {
      164,
      new BAICode(164, BAICodeType.Credit, "CTP CREDIT")
    },
    {
      165,
      new BAICode(165, BAICodeType.Credit, "ACH Received")
    },
    {
      166,
      new BAICode(166, BAICodeType.Credit, "ACH Originated Settlement")
    },
    {
      167,
      new BAICode(167, BAICodeType.Skip, "Total ACH Settlement Credits")
    },
    {
      168,
      new BAICode(168, BAICodeType.Credit, "ACH Originated Settlement - Return")
    },
    {
      169,
      new BAICode(169, BAICodeType.Credit, "ACH Miscellaneous")
    },
    {
      170,
      new BAICode(170, BAICodeType.Skip, "Total Other Check Deposits")
    },
    {
      171,
      new BAICode(171, BAICodeType.Credit, "Loan Deposit")
    },
    {
      172,
      new BAICode(172, BAICodeType.Credit, "Deposit Correction")
    },
    {
      173,
      new BAICode(173, BAICodeType.Credit, "Bank Prepared Deposit")
    },
    {
      174,
      new BAICode(174, BAICodeType.Credit, "Other Deposit")
    },
    {
      175,
      new BAICode(175, BAICodeType.Credit, "INDIV CHECK DEPOSIT PACKAGES")
    },
    {
      176 /*0xB0*/,
      new BAICode(176 /*0xB0*/, BAICodeType.Credit, "TOTAL NUMBER CHECKS DEPOSITED")
    },
    {
      178,
      new BAICode(178, BAICodeType.Skip, "LIST POST CREDITS")
    },
    {
      180,
      new BAICode(180, BAICodeType.Skip, "TOTAL LOAN PROCEEDS")
    },
    {
      182,
      new BAICode(182, BAICodeType.Skip, "TOTAL BANK - PREPARED DEPOSITS")
    },
    {
      184,
      new BAICode(184, BAICodeType.Credit, "DRAFT DEPOSITS")
    },
    {
      185,
      new BAICode(185, BAICodeType.Skip, "TOTAL MISC DEPOSITS")
    },
    {
      186,
      new BAICode(186, BAICodeType.Skip, "TOTAL CASH LETTER CREDITS")
    },
    {
      187,
      new BAICode(187, BAICodeType.Credit, "Check Deposit")
    },
    {
      188,
      new BAICode(188, BAICodeType.Skip, "TOTAL CASH LETTER ADJUSTMENTS")
    },
    {
      189,
      new BAICode(189, BAICodeType.Credit, "CASH LETTER ADJUSTMENTS")
    },
    {
      190,
      new BAICode(190, BAICodeType.Skip, "Total Incoming Money Transfers")
    },
    {
      191,
      new BAICode(191, BAICodeType.Credit, "IND INCOMING INTERNAL MON TFR")
    },
    {
      195,
      new BAICode(195, BAICodeType.Credit, "Money Transfer")
    },
    {
      196,
      new BAICode(196, BAICodeType.Credit, "Money Transfer - Returns")
    },
    {
      197,
      new BAICode(197, BAICodeType.Credit, "Email or Mobile Credit")
    },
    {
      198,
      new BAICode(198, BAICodeType.Credit, "COMPENSATION")
    },
    {
      199,
      new BAICode(199, BAICodeType.Skip, "Total Email or Mobile Credits")
    },
    {
      200,
      new BAICode(200, BAICodeType.Skip, "TOTAL AUTOMATIC TRANSFER CREDITS")
    },
    {
      201,
      new BAICode(201, BAICodeType.Credit, "Transfer - Automatic")
    },
    {
      202,
      new BAICode(202, BAICodeType.Credit, "BOND OPERATIONS CREDIT")
    },
    {
      205,
      new BAICode(205, BAICodeType.Skip, "TOTAL BOOK TRANSFER CREDITS")
    },
    {
      206,
      new BAICode(206, BAICodeType.Credit, "Money Transfer - Book")
    },
    {
      207,
      new BAICode(207, BAICodeType.Skip, "Total International Money Transfer Credits")
    },
    {
      208 /*0xD0*/,
      new BAICode(208 /*0xD0*/, BAICodeType.Credit, "Money Transfer - Cross-Border")
    },
    {
      210,
      new BAICode(210, BAICodeType.Skip, "Total International Credits")
    },
    {
      212,
      new BAICode(212, BAICodeType.Credit, "FOREIGN LETTERS OF CREDIT")
    },
    {
      213,
      new BAICode(213, BAICodeType.Credit, "Letter of Credit")
    },
    {
      214,
      new BAICode(214, BAICodeType.Credit, "Money Transfer - Foreign Exchange")
    },
    {
      215,
      new BAICode(215, BAICodeType.Skip, "Total Letters of Credit")
    },
    {
      216,
      new BAICode(216, BAICodeType.Credit, "Foreign Remittance")
    },
    {
      218,
      new BAICode(218, BAICodeType.Credit, "Foreign Collection")
    },
    {
      221,
      new BAICode(221, BAICodeType.Credit, "FOREIGN CHECK PURCHASE")
    },
    {
      222,
      new BAICode(222, BAICodeType.Credit, "FOREIGN CHECK DEPOSITED")
    },
    {
      224 /*0xE0*/,
      new BAICode(224 /*0xE0*/, BAICodeType.Credit, "Commission")
    },
    {
      226,
      new BAICode(226, BAICodeType.Credit, "INTERNATIONAL MONEY MARKET TRADING")
    },
    {
      227,
      new BAICode(227, BAICodeType.Credit, "Standing Order")
    },
    {
      229,
      new BAICode(229, BAICodeType.Credit, "MISC. INTERNATIONAL CREDIT")
    },
    {
      230,
      new BAICode(230, BAICodeType.Skip, "Total Security Credits")
    },
    {
      231,
      new BAICode(231, BAICodeType.Skip, "TOTAL COLLECTION CREDITS")
    },
    {
      232,
      new BAICode(232, BAICodeType.Credit, "Sale of Debt Security")
    },
    {
      233,
      new BAICode(233, BAICodeType.Credit, "Securities Sold")
    },
    {
      234,
      new BAICode(234, BAICodeType.Credit, "SALE OF EQUITY SECURITY")
    },
    {
      235,
      new BAICode(235, BAICodeType.Credit, "Matured Reverse Repurchase Order")
    },
    {
      236,
      new BAICode(236, BAICodeType.Credit, "Maturity of Debt Security")
    },
    {
      237,
      new BAICode(237, BAICodeType.Credit, "Collection")
    },
    {
      238,
      new BAICode(238, BAICodeType.Credit, "Collection of Dividends")
    },
    {
      239,
      new BAICode(239, BAICodeType.Skip, "TOTAL BANKER'S ACCEPTANCE CREDITS")
    },
    {
      240 /*0xF0*/,
      new BAICode(240 /*0xF0*/, BAICodeType.Credit, "COLLECTION OF COUPONS - BANKS")
    },
    {
      241,
      new BAICode(241, BAICodeType.Credit, "BANKER'S ACCEPTANCE")
    },
    {
      242,
      new BAICode(242, BAICodeType.Credit, "Collection of Interest Income")
    },
    {
      243,
      new BAICode(243, BAICodeType.Credit, "Matured Fed Funds Purchased")
    },
    {
      244,
      new BAICode(244, BAICodeType.Credit, "Interest/Matured Principal Payment")
    },
    {
      245,
      new BAICode(245, BAICodeType.Skip, "MONTHLY DIVIDENDS")
    },
    {
      246,
      new BAICode(246, BAICodeType.Credit, "COMMERCIAL PAPER")
    },
    {
      247,
      new BAICode(247, BAICodeType.Credit, "CAPITAL CHANGE")
    },
    {
      248,
      new BAICode(248, BAICodeType.Credit, "SAVINGS BONDS SALES ADJUSTMENT")
    },
    {
      249,
      new BAICode(249, BAICodeType.Credit, "Miscellaneous Security")
    },
    {
      250,
      new BAICode(250, BAICodeType.Skip, "Total Checks Posted and Returned")
    },
    {
      251,
      new BAICode(251, BAICodeType.Skip, "Total Debit Reversals")
    },
    {
      252,
      new BAICode(252, BAICodeType.Credit, "Reversal")
    },
    {
      254,
      new BAICode(254, BAICodeType.Credit, "Posting Error")
    },
    {
      (int) byte.MaxValue,
      new BAICode((int) byte.MaxValue, BAICodeType.Credit, "Check Return")
    },
    {
      256 /*0x0100*/,
      new BAICode(256 /*0x0100*/, BAICodeType.Skip, "Total ACH Return Items")
    },
    {
      257,
      new BAICode(257, BAICodeType.Credit, "ACH Received - Return")
    },
    {
      258,
      new BAICode(258, BAICodeType.Credit, "ACH REVERSAL CREDIT")
    },
    {
      260,
      new BAICode(260, BAICodeType.Skip, "TOTAL REJECTED CREDITS")
    },
    {
      261,
      new BAICode(261, BAICodeType.Credit, "IND. REJECTED CREDITS")
    },
    {
      263,
      new BAICode(263, BAICodeType.Credit, "Overdraft")
    },
    {
      266,
      new BAICode(266, BAICodeType.Credit, "Return Item")
    },
    {
      268,
      new BAICode(268, BAICodeType.Credit, "RETURN ITEM ADJUSTED")
    },
    {
      270,
      new BAICode(270, BAICodeType.Skip, "Total ZBA Credits")
    },
    {
      271,
      new BAICode(271, BAICodeType.Skip, "NET ZERO BALANCE AMOUNT")
    },
    {
      274,
      new BAICode(274, BAICodeType.Credit, "CUMULATIVE ZBA OR DISBURSEMENT CREDITS")
    },
    {
      275,
      new BAICode(275, BAICodeType.Credit, "Transfer - ZBA")
    },
    {
      276,
      new BAICode(276, BAICodeType.Credit, "CMA FLOAT ADJUSTMENT")
    },
    {
      277,
      new BAICode(277, BAICodeType.Credit, "Transfer - Controlled Disb Funding")
    },
    {
      278,
      new BAICode(278, BAICodeType.Credit, "Adjustment - ZBA")
    },
    {
      280,
      new BAICode(280, BAICodeType.Skip, "Total Controlled Disbursing Credits")
    },
    {
      281,
      new BAICode(281, BAICodeType.Credit, "Individual Controlled Disbursing Credit")
    },
    {
      285,
      new BAICode(285, BAICodeType.Skip, "TOTAL DTC DISBURSEMENT CREDITS")
    },
    {
      286,
      new BAICode(286, BAICodeType.Credit, "IND. DTC DISBURSEMENT CREDITS")
    },
    {
      294,
      new BAICode(294, BAICodeType.Skip, "Total ATM Credits")
    },
    {
      295,
      new BAICode(295, BAICodeType.Credit, "ATM")
    },
    {
      301,
      new BAICode(301, BAICodeType.Credit, "Deposit")
    },
    {
      302,
      new BAICode(302, BAICodeType.Skip, "CORRESPONDENT BANK DEPOSITS")
    },
    {
      303,
      new BAICode(303, BAICodeType.Skip, "TOTAL WIRE TRANSFERS IN - FF")
    },
    {
      304,
      new BAICode(304, BAICodeType.Skip, "TOTAL WIRE TRANSFERS IN - CHF")
    },
    {
      305,
      new BAICode(305, BAICodeType.Skip, "TOTAL FED FUNDS SOLD")
    },
    {
      306,
      new BAICode(306, BAICodeType.Credit, "FED FUNDS SOLD")
    },
    {
      307,
      new BAICode(307, BAICodeType.Skip, "Total Trust Credits")
    },
    {
      308,
      new BAICode(308, BAICodeType.Credit, "Trust Credit")
    },
    {
      309,
      new BAICode(309, BAICodeType.Skip, "VALUE DATED FUNDS")
    },
    {
      310,
      new BAICode(310, BAICodeType.Skip, "TOTAL COMMERCIAL DEPOSITS")
    },
    {
      315,
      new BAICode(315, BAICodeType.Skip, "TOTAL INTERNATIONAL CREDTS-FF")
    },
    {
      316,
      new BAICode(316, BAICodeType.Skip, "TOTAL INTERNATIONAL CRDTS-CHF")
    },
    {
      318,
      new BAICode(318, BAICodeType.Skip, "TOTAL FOREIGN CHECK PURCHASED")
    },
    {
      319,
      new BAICode(319, BAICodeType.Skip, "LATE DEPOSIT")
    },
    {
      320,
      new BAICode(320, BAICodeType.Skip, "TOTAL SECURITIES SOLD - FF")
    },
    {
      321,
      new BAICode(321, BAICodeType.Skip, "TOTAL SECURITIES SOLD - CHF")
    },
    {
      324,
      new BAICode(324, BAICodeType.Skip, "TOTAL SECURITIES MATURED - FF")
    },
    {
      325,
      new BAICode(325, BAICodeType.Skip, "TOTAL SECURITIES MATURED -CHF")
    },
    {
      326,
      new BAICode(326, BAICodeType.Skip, "TOTAL SECURITIES INTEREST")
    },
    {
      327,
      new BAICode(327, BAICodeType.Skip, "TOTAL SECURITIES MATURED")
    },
    {
      328,
      new BAICode(328, BAICodeType.Skip, "TOTAL SECURITIES INTEREST-FF")
    },
    {
      329,
      new BAICode(329, BAICodeType.Skip, "TOTAL SECURITIES INTEREST-CHF")
    },
    {
      330,
      new BAICode(330, BAICodeType.Skip, "TOTAL ESCROW CREDITS")
    },
    {
      331,
      new BAICode(331, BAICodeType.Credit, "IND. ESCROW CREDITS")
    },
    {
      332,
      new BAICode(332, BAICodeType.Skip, "TOTAL MISC SEC CREDITS - FF")
    },
    {
      336,
      new BAICode(336, BAICodeType.Skip, "TOTAL MISC SEC CREDITS - CHF")
    },
    {
      338,
      new BAICode(338, BAICodeType.Skip, "TOTAL SECURITIES SOLD")
    },
    {
      340,
      new BAICode(340, BAICodeType.Skip, "TOTAL BROKER DEPOSITS")
    },
    {
      341,
      new BAICode(341, BAICodeType.Skip, "TOTAL BROKER DEPOSITS - FF")
    },
    {
      342,
      new BAICode(342, BAICodeType.Credit, "BROKERS DEPOSIT")
    },
    {
      343,
      new BAICode(343, BAICodeType.Skip, "TOTAL BROKER DEPOSITS - CHF")
    },
    {
      344,
      new BAICode(344, BAICodeType.Credit, "IND. BACK VALUED CREDITS")
    },
    {
      345,
      new BAICode(345, BAICodeType.Credit, "ITEM IN BROKERS DEPOSIT")
    },
    {
      346,
      new BAICode(346, BAICodeType.Credit, "Sweep Interest Income")
    },
    {
      347,
      new BAICode(347, BAICodeType.Credit, "Sweep from Investment or Loan")
    },
    {
      348,
      new BAICode(348, BAICodeType.Credit, "FUTURES CREDIT")
    },
    {
      349,
      new BAICode(349, BAICodeType.Credit, "Principal Payments")
    },
    {
      350,
      new BAICode(350, BAICodeType.Skip, "INVESTMENT SOLD")
    },
    {
      351,
      new BAICode(351, BAICodeType.Credit, "Investment Sold")
    },
    {
      352,
      new BAICode(352, BAICodeType.Skip, "TOTAL CASH CENTER CREDITS")
    },
    {
      353,
      new BAICode(353, BAICodeType.Credit, "CASH CENTER CREDIT")
    },
    {
      354,
      new BAICode(354, BAICodeType.Credit, "Interest")
    },
    {
      355,
      new BAICode(355, BAICodeType.Skip, "Total Investment Interest Credits")
    },
    {
      356,
      new BAICode(356, BAICodeType.Skip, "Total Credit Adjustments")
    },
    {
      357,
      new BAICode(357, BAICodeType.Credit, "Adjustment")
    },
    {
      358,
      new BAICode(358, BAICodeType.Credit, "YTD ADJUSTMENT CREDIT")
    },
    {
      359,
      new BAICode(359, BAICodeType.Credit, "INTEREST ADJUSTMENT CREDIT")
    },
    {
      360,
      new BAICode(360, BAICodeType.Skip, "TOT CR LESS W/TFR & RET CHKS")
    },
    {
      361,
      new BAICode(361, BAICodeType.Skip, "GR TOT CR LESS GR TOT DR")
    },
    {
      362,
      new BAICode(362, BAICodeType.Credit, "CORRESPONDENT COLLECTION")
    },
    {
      363,
      new BAICode(363, BAICodeType.Credit, "CORRESPONDENT COLLECTION ADJUSTMENT")
    },
    {
      364,
      new BAICode(364, BAICodeType.Credit, "LOAN PARTICIPATION")
    },
    {
      366,
      new BAICode(366, BAICodeType.Credit, "Cash Deposit")
    },
    {
      367,
      new BAICode(367, BAICodeType.Credit, "Food Stamps")
    },
    {
      368,
      new BAICode(368, BAICodeType.Credit, "FOOD STAMP ADJUSTMENT")
    },
    {
      369,
      new BAICode(369, BAICodeType.Credit, "CLEARING SETTLEMENT CREDIT")
    },
    {
      370,
      new BAICode(370, BAICodeType.Skip, "TOTAL BACK VALUE CREDITS")
    },
    {
      371,
      new BAICode(371, BAICodeType.Skip, "Total Fees Credit")
    },
    {
      372,
      new BAICode(372, BAICodeType.Credit, "BACK VALUE ADJUSTMENT")
    },
    {
      373,
      new BAICode(373, BAICodeType.Credit, "CUSTOMER PAYROLL")
    },
    {
      374,
      new BAICode(374, BAICodeType.Credit, "FRB STATEMENT RECAP")
    },
    {
      376,
      new BAICode(376, BAICodeType.Credit, "SAVINGS BOND LETTER OR ADJUSTMENT")
    },
    {
      377,
      new BAICode(377, BAICodeType.Credit, "TREASURY TAX AND LOAN CREDIT")
    },
    {
      378,
      new BAICode(378, BAICodeType.Credit, "TRANSFER OF TREASURY CREDIT")
    },
    {
      379,
      new BAICode(379, BAICodeType.Credit, "FRB GOVERNMENT CHECKS CASH LETTER CREDIT")
    },
    {
      381,
      new BAICode(381, BAICodeType.Credit, "FRB GOVERNMENT CHECK ADJUSTMENT")
    },
    {
      382,
      new BAICode(382, BAICodeType.Credit, "FRB POSTAL MONEY ORDER CREDIT")
    },
    {
      383,
      new BAICode(383, BAICodeType.Credit, "FRB POSTAL MONEY ORDER ADJUSTMENT")
    },
    {
      384,
      new BAICode(384, BAICodeType.Credit, "FRB CASH LETTER AUTO CHARGE CREDIT")
    },
    {
      385,
      new BAICode(385, BAICodeType.Skip, "TOTAL UNIVERSAL CREDIT")
    },
    {
      386,
      new BAICode(386, BAICodeType.Credit, "FRB CASH LETTER AUTO CHARGE ADJUSTMENT")
    },
    {
      387,
      new BAICode(387, BAICodeType.Credit, "FRB FINE-SORT CASH LETTER CREDIT")
    },
    {
      388,
      new BAICode(388, BAICodeType.Credit, "FRB FINE-SORT ADJUSTMENT")
    },
    {
      389,
      new BAICode(389, BAICodeType.Skip, "TOTAL FREIGHT PAYMENT CREDITS")
    },
    {
      390,
      new BAICode(390, BAICodeType.Skip, "Total Miscellaneous Credits")
    },
    {
      391,
      new BAICode(391, BAICodeType.Credit, "UNIVERSAL CREDIT")
    },
    {
      392,
      new BAICode(392, BAICodeType.Credit, "FREIGHT PAYMENT CREDIT")
    },
    {
      393,
      new BAICode(393, BAICodeType.Credit, "ITEMIZED CREDIT OVER 10")
    },
    {
      394,
      new BAICode(394, BAICodeType.Credit, "CUMULATIVE CREDITS")
    },
    {
      395,
      new BAICode(395, BAICodeType.Credit, "Check Reversal")
    },
    {
      397,
      new BAICode(397, BAICodeType.Credit, "Adjustment - Float")
    },
    {
      398,
      new BAICode(398, BAICodeType.Credit, "Fee - Reversal")
    },
    {
      399,
      new BAICode(399, BAICodeType.Credit, "Miscellaneous Credit")
    },
    {
      400,
      new BAICode(400, BAICodeType.Skip, "Total Debits")
    },
    {
      401,
      new BAICode(401, BAICodeType.Skip, "MTD TOTAL DEBIT AMOUNT")
    },
    {
      403,
      new BAICode(403, BAICodeType.Skip, "TODAY'S TOTAL DEBITS")
    },
    {
      405,
      new BAICode(405, BAICodeType.Skip, "TOT DR LESS W/TFR & CHGBACKS")
    },
    {
      406,
      new BAICode(406, BAICodeType.Skip, "TOTAL DEBITS - 2 OR MORE DAYS")
    },
    {
      408,
      new BAICode(408, BAICodeType.Debit, "Adjustment - Float")
    },
    {
      409,
      new BAICode(409, BAICodeType.Debit, "DEBIT (ANY TYPE)")
    },
    {
      410,
      new BAICode(410, BAICodeType.Skip, "TOTAL YTD ADJUSTMENT DEBITS")
    },
    {
      412,
      new BAICode(412, BAICodeType.Skip, "TOTAL DEBITS (EXCLUDING RETURNED ITEMS)")
    },
    {
      415,
      new BAICode(415, BAICodeType.Debit, "Adjustment - Lockbox")
    },
    {
      416,
      new BAICode(416, BAICodeType.Skip, "Total Lockbox Debits")
    },
    {
      420,
      new BAICode(420, BAICodeType.Skip, "EDI TRANSACTION DEBITS")
    },
    {
      421,
      new BAICode(421, BAICodeType.Debit, "EDI TRANSACTION DEBIT")
    },
    {
      422,
      new BAICode(422, BAICodeType.Debit, "EDIBANX SETTLEMENT DEBIT")
    },
    {
      423,
      new BAICode(423, BAICodeType.Debit, "EDIBANX RETURN ITEM DEBIT")
    },
    {
      430,
      new BAICode(430, BAICodeType.Skip, "TOTAL PAYABLE THRU DRAFTS")
    },
    {
      435,
      new BAICode(435, BAICodeType.Debit, "PAYABLES THRU DRAFTS")
    },
    {
      438,
      new BAICode(438, BAICodeType.Skip, "Total SEPA Debits")
    },
    {
      439,
      new BAICode(439, BAICodeType.Debit, "SEPA Return Debit")
    },
    {
      441,
      new BAICode(441, BAICodeType.Debit, "SEPA Direct Debit")
    },
    {
      444,
      new BAICode(444, BAICodeType.Debit, "SEPA Adjustment Debit")
    },
    {
      445,
      new BAICode(445, BAICodeType.Debit, "ACH CONCENTRATION DEBIT")
    },
    {
      446,
      new BAICode(446, BAICodeType.Skip, "TOTAL ACH DISBURSEMENT FUNDING DEBITS")
    },
    {
      447,
      new BAICode(447, BAICodeType.Debit, "ACH DISBURSEMENT FUNDING DEBIT")
    },
    {
      450,
      new BAICode(450, BAICodeType.Skip, "Total ACH Debits")
    },
    {
      451,
      new BAICode(451, BAICodeType.Debit, "ITEM IN ACH DISBURSEMENT OR DEBIT")
    },
    {
      452,
      new BAICode(452, BAICodeType.Debit, "DEBIT")
    },
    {
      455,
      new BAICode(455, BAICodeType.Debit, "ACH Received")
    },
    {
      458,
      new BAICode(458, BAICodeType.Debit, "Real Time Payment")
    },
    {
      459,
      new BAICode(459, BAICodeType.Skip, "Total Real Time Payment Debits")
    },
    {
      462,
      new BAICode(462, BAICodeType.Debit, "ACCOUNT HOLDER INIT ACH DEBIT")
    },
    {
      463,
      new BAICode(463, BAICodeType.Skip, "CTP DEBITS")
    },
    {
      464,
      new BAICode(464, BAICodeType.Debit, "CTP DEBIT")
    },
    {
      465,
      new BAICode(465, BAICodeType.Skip, "CTP SETTLEMENT")
    },
    {
      466,
      new BAICode(466, BAICodeType.Debit, "ACH Originated Settlement")
    },
    {
      467,
      new BAICode(467, BAICodeType.Skip, "Total ACH Settlement Debits")
    },
    {
      468,
      new BAICode(468, BAICodeType.Debit, "ACH Originated Settlement - Return")
    },
    {
      469,
      new BAICode(469, BAICodeType.Debit, "ACH Miscellaneous")
    },
    {
      470,
      new BAICode(470, BAICodeType.Skip, "Total Check Paid")
    },
    {
      471,
      new BAICode(471, BAICodeType.Skip, "TOTAL CHECKS PAID-CUMLTVE MTD")
    },
    {
      472,
      new BAICode(472, BAICodeType.Debit, "Check Paid - Cumulative")
    },
    {
      474,
      new BAICode(474, BAICodeType.Debit, "CERTIFIED CHECK DEBIT")
    },
    {
      475,
      new BAICode(475, BAICodeType.Debit, "Check Paid")
    },
    {
      476,
      new BAICode(476, BAICodeType.Debit, "FED RES BANK LETTER DEBIT")
    },
    {
      477,
      new BAICode(477, BAICodeType.Debit, "Bank Prepared Debit")
    },
    {
      478,
      new BAICode(478, BAICodeType.Skip, "LIST POST DEBIT")
    },
    {
      479,
      new BAICode(479, BAICodeType.Debit, "LIST POST DEBIT")
    },
    {
      480,
      new BAICode(480, BAICodeType.Skip, "TOTAL LOAN PAYMENTS")
    },
    {
      481,
      new BAICode(481, BAICodeType.Debit, "Loan Payment")
    },
    {
      482,
      new BAICode(482, BAICodeType.Skip, "TOTAL BANK - ORIGINATED DEBITS")
    },
    {
      484,
      new BAICode(484, BAICodeType.Debit, "Check Draft")
    },
    {
      485,
      new BAICode(485, BAICodeType.Debit, "DTC DEBIT")
    },
    {
      486,
      new BAICode(486, BAICodeType.Skip, "TOTAL CASH LETTER DEBITS")
    },
    {
      487,
      new BAICode(487, BAICodeType.Debit, "Cash Letter")
    },
    {
      489,
      new BAICode(489, BAICodeType.Debit, "CASH LETTER ADJUSTMENT")
    },
    {
      490,
      new BAICode(490, BAICodeType.Skip, "Total Outgoing Money Transfers")
    },
    {
      491,
      new BAICode(491, BAICodeType.Debit, "IND OUTGOING INTERNAL MON TFR")
    },
    {
      493,
      new BAICode(493, BAICodeType.Debit, "CUSTOMER TERMINAL INIT MONEY TRANSFER")
    },
    {
      495,
      new BAICode(495, BAICodeType.Debit, "Money Transfer")
    },
    {
      496,
      new BAICode(496, BAICodeType.Debit, "Money Transfer - Returns")
    },
    {
      497,
      new BAICode(497, BAICodeType.Debit, "Email or Mobile Debit")
    },
    {
      498,
      new BAICode(498, BAICodeType.Debit, "COMPENSATION")
    },
    {
      499,
      new BAICode(499, BAICodeType.Skip, "Total Email or Mobile Debits")
    },
    {
      500,
      new BAICode(500, BAICodeType.Skip, "TOTAL AUTOMATIC TRANSFER DEBITS")
    },
    {
      501,
      new BAICode(501, BAICodeType.Debit, "Transfer - Automatic")
    },
    {
      502,
      new BAICode(502, BAICodeType.Debit, "BOND OPERATIONS DEBIT")
    },
    {
      505,
      new BAICode(505, BAICodeType.Skip, "TOTAL BOOK TRANSFER DEBITS")
    },
    {
      506,
      new BAICode(506, BAICodeType.Debit, "Money Transfer - Book")
    },
    {
      507,
      new BAICode(507, BAICodeType.Skip, "Total International Money Transfer Debits")
    },
    {
      508,
      new BAICode(508, BAICodeType.Debit, "Money Transfer - Cross-Border")
    },
    {
      510,
      new BAICode(510, BAICodeType.Skip, "Total International Debits")
    },
    {
      512 /*0x0200*/,
      new BAICode(512 /*0x0200*/, BAICodeType.Debit, "Letter of Credit - Commercial")
    },
    {
      513,
      new BAICode(513, BAICodeType.Debit, "Letter of Credit")
    },
    {
      514,
      new BAICode(514, BAICodeType.Debit, "Money Transfer - Foreign Exchange")
    },
    {
      515,
      new BAICode(515, BAICodeType.Skip, "Total Letters of Credit")
    },
    {
      516,
      new BAICode(516, BAICodeType.Debit, "Foreign Remittance")
    },
    {
      518,
      new BAICode(518, BAICodeType.Debit, "Foreign Collection")
    },
    {
      522,
      new BAICode(522, BAICodeType.Debit, "FOREIGN CHECKS PAID")
    },
    {
      524,
      new BAICode(524, BAICodeType.Debit, "Commission")
    },
    {
      526,
      new BAICode(526, BAICodeType.Debit, "International Money Market Trading")
    },
    {
      527,
      new BAICode(527, BAICodeType.Debit, "Standing Order")
    },
    {
      529,
      new BAICode(529, BAICodeType.Debit, "MISC INTERNATIONAL DEBIT")
    },
    {
      530,
      new BAICode(530, BAICodeType.Skip, "Total Security Debits")
    },
    {
      531,
      new BAICode(531, BAICodeType.Debit, "Securities Purchased")
    },
    {
      532,
      new BAICode(532, BAICodeType.Skip, "TOT AMT OF SECURITIES PURCHSD")
    },
    {
      533,
      new BAICode(533, BAICodeType.Debit, "SECURITY COLLECTION DEBIT")
    },
    {
      534,
      new BAICode(534, BAICodeType.Skip, "TOT MISC SECURITIES DEBITS-FF")
    },
    {
      535,
      new BAICode(535, BAICodeType.Debit, "PURCHASE OF EQUITY SECURITIES")
    },
    {
      536,
      new BAICode(536, BAICodeType.Skip, "TOT MISC SECURTIES DEBITS-CHF")
    },
    {
      537,
      new BAICode(537, BAICodeType.Skip, "TOTAL COLLECTION DEBIT")
    },
    {
      538,
      new BAICode(538, BAICodeType.Debit, "MATURED REPURCHASE ORDER")
    },
    {
      539,
      new BAICode(539, BAICodeType.Skip, "TOTAL BANKERS' ACCEPTANCE DEBIT")
    },
    {
      540,
      new BAICode(540, BAICodeType.Debit, "COUPON COLLECTION DEBIT")
    },
    {
      541,
      new BAICode(541, BAICodeType.Debit, "BANKERS' ACCEPTANCES")
    },
    {
      542,
      new BAICode(542, BAICodeType.Debit, "Purchase of Debt Securities")
    },
    {
      543,
      new BAICode(543, BAICodeType.Debit, "DOMESTIC COLLECTION")
    },
    {
      544,
      new BAICode(544, BAICodeType.Debit, "Interest/Matured Principal Payment")
    },
    {
      546,
      new BAICode(546, BAICodeType.Debit, "COMMERCIAL PAPER")
    },
    {
      547,
      new BAICode(547, BAICodeType.Debit, "CAPITAL CHANGE")
    },
    {
      548,
      new BAICode(548, BAICodeType.Debit, "SAVINGS BOND SALES ADJUSTMENT")
    },
    {
      549,
      new BAICode(549, BAICodeType.Debit, "Miscellaneous Security")
    },
    {
      550,
      new BAICode(550, BAICodeType.Skip, "Total Deposited Items Returned")
    },
    {
      551,
      new BAICode(551, BAICodeType.Skip, "Total Credit Reversals")
    },
    {
      552,
      new BAICode(552, BAICodeType.Debit, "Reversal")
    },
    {
      554,
      new BAICode(554, BAICodeType.Debit, "Posting Error")
    },
    {
      555,
      new BAICode(555, BAICodeType.Debit, "Deposited Item Returned")
    },
    {
      556,
      new BAICode(556, BAICodeType.Skip, "Total ACH Return Items")
    },
    {
      557,
      new BAICode(557, BAICodeType.Debit, "ACH Received - Return")
    },
    {
      558,
      new BAICode(558, BAICodeType.Debit, "ACH REVERSAL DEBIT")
    },
    {
      560,
      new BAICode(560, BAICodeType.Skip, "TOTAL REJECTED DEBITS")
    },
    {
      561,
      new BAICode(561, BAICodeType.Debit, "IND. REJECTED DEBITS")
    },
    {
      563,
      new BAICode(563, BAICodeType.Debit, "Overdraft")
    },
    {
      564,
      new BAICode(564, BAICodeType.Debit, "Fee - Overdraft")
    },
    {
      566,
      new BAICode(566, BAICodeType.Debit, "Return Item")
    },
    {
      567,
      new BAICode(567, BAICodeType.Debit, "Fee - Return Item")
    },
    {
      568,
      new BAICode(568, BAICodeType.Debit, "RETURN ITEM ADJUSTMENT")
    },
    {
      570,
      new BAICode(570, BAICodeType.Skip, "Total ZBA Debits")
    },
    {
      574,
      new BAICode(574, BAICodeType.Debit, "CUMULATIVE ZBA OR DISBURSEMENT DEBITS")
    },
    {
      575,
      new BAICode(575, BAICodeType.Debit, "Transfer - ZBA")
    },
    {
      577,
      new BAICode(577, BAICodeType.Debit, "Transfer - Controlled Disb Funding")
    },
    {
      578,
      new BAICode(578, BAICodeType.Debit, "Adjustment - ZBA")
    },
    {
      580,
      new BAICode(580, BAICodeType.Skip, "Total Controlled Disbursement Debits")
    },
    {
      581,
      new BAICode(581, BAICodeType.Debit, "Controlled Disb Check")
    },
    {
      583,
      new BAICode(583, BAICodeType.Skip, "Total Cont Disb First Presentment")
    },
    {
      584,
      new BAICode(584, BAICodeType.Skip, "Total Cont Disb Second Presentment")
    },
    {
      585,
      new BAICode(585, BAICodeType.Skip, "Total Cont Disb Funding Rqmt")
    },
    {
      586,
      new BAICode(586, BAICodeType.Skip, "FRB PRESENTMENT ESTIMATE")
    },
    {
      587,
      new BAICode(587, BAICodeType.Skip, "Total Cont Disb Late Debits")
    },
    {
      588,
      new BAICode(588, BAICodeType.Skip, "TOTAL DISBURSEMENT CHECK PAID - LATE AMT")
    },
    {
      590,
      new BAICode(590, BAICodeType.Skip, "TOTAL DTC DEBITS")
    },
    {
      594,
      new BAICode(594, BAICodeType.Skip, "Total ATM Debits")
    },
    {
      595,
      new BAICode(595, BAICodeType.Debit, "ATM")
    },
    {
      596,
      new BAICode(596, BAICodeType.Skip, "TOTAL ARP DEBITS")
    },
    {
      597,
      new BAICode(597, BAICodeType.Debit, "ARP DEBIT")
    },
    {
      601,
      new BAICode(601, BAICodeType.Skip, "ESTIMATED TOTAL DISBURSEMENTS")
    },
    {
      602,
      new BAICode(602, BAICodeType.Skip, "Total Adjusted Disbursement")
    },
    {
      610,
      new BAICode(610, BAICodeType.Skip, "TOTAL FUNDS REQUIRED")
    },
    {
      611,
      new BAICode(611, BAICodeType.Skip, "TOTAL WIRE TRANSFERS OUT -CHF")
    },
    {
      612,
      new BAICode(612, BAICodeType.Skip, "TOTAL WIRE TRANSFERS OUT - FF")
    },
    {
      613,
      new BAICode(613, BAICodeType.Skip, "TOT INTERNATIONAL DEBITS-CHF")
    },
    {
      614,
      new BAICode(614, BAICodeType.Skip, "TOT INTERNATIONAL DEBITS-FF")
    },
    {
      615,
      new BAICode(615, BAICodeType.Skip, "TOT FRB - COMMRL BANK DEBITS")
    },
    {
      616,
      new BAICode(616, BAICodeType.Debit, "FRB - COMMERCIAL BANK DEBITS")
    },
    {
      617,
      new BAICode(617, BAICodeType.Skip, "TOTAL SECURITIES PURCHASED -CHF")
    },
    {
      618,
      new BAICode(618, BAICodeType.Skip, "TOTAL SECURITIES PURCHASED-FF")
    },
    {
      621,
      new BAICode(621, BAICodeType.Skip, "TOTAL BROKER DEBITS - CHF")
    },
    {
      622,
      new BAICode(622, BAICodeType.Debit, "BROKER DEBITS")
    },
    {
      623,
      new BAICode(623, BAICodeType.Skip, "TOTAL BROKER DEBITS - FF")
    },
    {
      625,
      new BAICode(625, BAICodeType.Skip, "TOTAL BROKER DEBITS")
    },
    {
      626,
      new BAICode(626, BAICodeType.Skip, "TOTAL FED FUNDS PURCHASED")
    },
    {
      627,
      new BAICode(627, BAICodeType.Debit, "FED FUNDS PURCHASED")
    },
    {
      628,
      new BAICode(628, BAICodeType.Skip, "Total Cash Center Debits")
    },
    {
      629,
      new BAICode(629, BAICodeType.Debit, "CASH CENTER DEBIT")
    },
    {
      630,
      new BAICode(630, BAICodeType.Skip, "Total Debit Adjustments")
    },
    {
      631,
      new BAICode(631, BAICodeType.Debit, "Adjustment")
    },
    {
      632,
      new BAICode(632, BAICodeType.Skip, "Total Trust Debits")
    },
    {
      633,
      new BAICode(633, BAICodeType.Debit, "Trust Debit")
    },
    {
      634,
      new BAICode(634, BAICodeType.Debit, "YTD ADJUSTMENT DEBIT")
    },
    {
      640,
      new BAICode(640, BAICodeType.Skip, "TOTAL ESCROW DEBITS")
    },
    {
      641,
      new BAICode(641, BAICodeType.Debit, "IND. ESCROW DEBIT")
    },
    {
      644,
      new BAICode(644, BAICodeType.Debit, "IND. BACK VALUE DEBIT")
    },
    {
      646,
      new BAICode(646, BAICodeType.Skip, "TRANSFER CALCULATION DEBIT")
    },
    {
      650,
      new BAICode(650, BAICodeType.Skip, "INVESTMENTS PURCHASED")
    },
    {
      651,
      new BAICode(651, BAICodeType.Debit, "Investment purchased")
    },
    {
      654,
      new BAICode(654, BAICodeType.Debit, "Interest")
    },
    {
      655,
      new BAICode(655, BAICodeType.Skip, "Total Investment Interest Debits")
    },
    {
      656,
      new BAICode(656, BAICodeType.Debit, "Sweep to Investment or Loan")
    },
    {
      657,
      new BAICode(657, BAICodeType.Debit, "FUTURES DEBIT")
    },
    {
      658,
      new BAICode(658, BAICodeType.Debit, "Principal Payments")
    },
    {
      659,
      new BAICode(659, BAICodeType.Debit, "INTEREST ADJUSTMENT DEBIT")
    },
    {
      661,
      new BAICode(661, BAICodeType.Debit, "Fee - Account Analysis")
    },
    {
      662,
      new BAICode(662, BAICodeType.Debit, "CORRESPONDENT COLLECTION DEBIT")
    },
    {
      663,
      new BAICode(663, BAICodeType.Debit, "CORRESPONDENT COLLECTION ADJUSTMENT")
    },
    {
      664,
      new BAICode(664, BAICodeType.Debit, "LOAN PARTICIPATION")
    },
    {
      665,
      new BAICode(665, BAICodeType.Skip, "INTERCEPT DEBITS")
    },
    {
      666,
      new BAICode(666, BAICodeType.Debit, "Currency and Coin Shipped")
    },
    {
      667,
      new BAICode(667, BAICodeType.Debit, "Food Stamps")
    },
    {
      668,
      new BAICode(668, BAICodeType.Debit, "FOOD STAMP ADJUSTMENT")
    },
    {
      669,
      new BAICode(669, BAICodeType.Debit, "CLEARING SETTLEMENT DEBIT")
    },
    {
      670,
      new BAICode(670, BAICodeType.Skip, "TOTAL BACK VALUE DEBITS")
    },
    {
      671,
      new BAICode(671, BAICodeType.Skip, "Total Fees Debit")
    },
    {
      672,
      new BAICode(672, BAICodeType.Debit, "BACK VALUE ADJUSTMENT")
    },
    {
      673,
      new BAICode(673, BAICodeType.Debit, "CUSTOMER PAYROLL")
    },
    {
      674,
      new BAICode(674, BAICodeType.Debit, "FRB STATEMENT RECAP")
    },
    {
      676,
      new BAICode(676, BAICodeType.Debit, "SAVINGS BOND LETTER OR ADUSTMENT")
    },
    {
      677,
      new BAICode(677, BAICodeType.Debit, "TREASURY TAX AND LOAN DEBIT")
    },
    {
      678,
      new BAICode(678, BAICodeType.Debit, "TRANSFER OF TREASURY DEBIT")
    },
    {
      679,
      new BAICode(679, BAICodeType.Debit, "FRB GOVERNMENT CHECK CASH LETTER DEBIT")
    },
    {
      681,
      new BAICode(681, BAICodeType.Debit, "FRB GOVERNMENT CHECK ADJUSTMENT")
    },
    {
      682,
      new BAICode(682, BAICodeType.Debit, "FRB POSTAL MONEY ORDER DEBIT")
    },
    {
      683,
      new BAICode(683, BAICodeType.Debit, "FRB POSTAL MONEY ORDER ADJUSTMENT")
    },
    {
      684,
      new BAICode(684, BAICodeType.Debit, "FRB CASH LETTER AUTO CHARGE DEBIT")
    },
    {
      685,
      new BAICode(685, BAICodeType.Skip, "TOTAL UNIVERSAL DEBITS")
    },
    {
      686,
      new BAICode(686, BAICodeType.Debit, "FRB CASH LETTER AUTO CHARGE ADJUSTMENT")
    },
    {
      687,
      new BAICode(687, BAICodeType.Debit, "FRB FINE-SORT CASH LETTER DEBIT")
    },
    {
      688,
      new BAICode(688, BAICodeType.Debit, "FRB FINE-SORT ADJUSTMENT")
    },
    {
      689,
      new BAICode(689, BAICodeType.Skip, "TOTAL FREIGHT PAYMENT DEBITS")
    },
    {
      690,
      new BAICode(690, BAICodeType.Skip, "Total Miscellaneous Debits")
    },
    {
      691,
      new BAICode(691, BAICodeType.Debit, "UNIVERSAL DEBIT")
    },
    {
      692,
      new BAICode(692, BAICodeType.Debit, "FREIGHT PAYMENT DEBIT")
    },
    {
      693,
      new BAICode(693, BAICodeType.Debit, "ITEMIZED DEBIT OVER 10")
    },
    {
      694,
      new BAICode(694, BAICodeType.Debit, "DEPOSIT REVERSAL")
    },
    {
      695,
      new BAICode(695, BAICodeType.Debit, "Deposit Correction")
    },
    {
      696,
      new BAICode(696, BAICodeType.Debit, "Collection")
    },
    {
      697,
      new BAICode(697, BAICodeType.Debit, "CUMULATIVE DEBITS")
    },
    {
      698,
      new BAICode(698, BAICodeType.Debit, "Fee - Charged")
    },
    {
      699,
      new BAICode(699, BAICodeType.Debit, "Miscellaneous Debit")
    },
    {
      701,
      new BAICode(701, BAICodeType.Skip, "Principal Loan Balance")
    },
    {
      703,
      new BAICode(703, BAICodeType.Skip, "Available Commitment Amount")
    },
    {
      705,
      new BAICode(705, BAICodeType.Skip, "Payment Amount Due")
    },
    {
      707,
      new BAICode(707, BAICodeType.Skip, "Principal Amount Past Due")
    },
    {
      709,
      new BAICode(709, BAICodeType.Skip, "Interest Amount Past Due")
    },
    {
      720,
      new BAICode(720, BAICodeType.Skip, "Total Loan Payment Credits")
    },
    {
      721,
      new BAICode(721, BAICodeType.Credit, "Loan Applied to Interest")
    },
    {
      722,
      new BAICode(722, BAICodeType.Credit, "Loan Applied to Principal")
    },
    {
      723,
      new BAICode(723, BAICodeType.Credit, "Loan Applied to Escrow")
    },
    {
      724,
      new BAICode(724, BAICodeType.Credit, "Loan Applied to Late Charges")
    },
    {
      725,
      new BAICode(725, BAICodeType.Credit, "Loan Applied to Buydown")
    },
    {
      726,
      new BAICode(726, BAICodeType.Credit, "Loan Applied to Misc. Fees")
    },
    {
      727,
      new BAICode(727, BAICodeType.Credit, "Loan Applied to Deferred Interest Detail")
    },
    {
      728,
      new BAICode(728, BAICodeType.Credit, "Loan Applied to Service Charge")
    },
    {
      760,
      new BAICode(760, BAICodeType.Skip, "Total Loan Disbursement Debits")
    },
    {
      890,
      new BAICode(890, BAICodeType.Skip, "Informational Message")
    },
    {
      900,
      new BAICode(900, BAICodeType.Skip, "User Defined Status Code 900")
    },
    {
      901,
      new BAICode(901, BAICodeType.Skip, "User Defined Status Code 901")
    },
    {
      902,
      new BAICode(902, BAICodeType.Skip, "User Defined Status Code 902")
    },
    {
      903,
      new BAICode(903, BAICodeType.Skip, "User Defined Status Code 903")
    },
    {
      904,
      new BAICode(904, BAICodeType.Skip, "User Defined Status Code 904")
    },
    {
      905,
      new BAICode(905, BAICodeType.Skip, "User Defined Status Code 905")
    },
    {
      906,
      new BAICode(906, BAICodeType.Skip, "User Defined Status Code 906")
    },
    {
      907,
      new BAICode(907, BAICodeType.Skip, "User Defined Status Code 907")
    },
    {
      908,
      new BAICode(908, BAICodeType.Skip, "User Defined Status Code 908")
    },
    {
      909,
      new BAICode(909, BAICodeType.Skip, "User Defined Status Code 909")
    },
    {
      910,
      new BAICode(910, BAICodeType.Skip, "User Defined Status Code 910")
    },
    {
      911,
      new BAICode(911, BAICodeType.Skip, "User Defined Status Code 911")
    },
    {
      912,
      new BAICode(912, BAICodeType.Skip, "User Defined Status Code 912")
    },
    {
      913,
      new BAICode(913, BAICodeType.Skip, "User Defined Status Code 913")
    },
    {
      914,
      new BAICode(914, BAICodeType.Skip, "User Defined Status Code 914")
    },
    {
      915,
      new BAICode(915, BAICodeType.Skip, "User Defined Status Code 915")
    },
    {
      916,
      new BAICode(916, BAICodeType.Skip, "User Defined Status Code 916")
    },
    {
      917,
      new BAICode(917, BAICodeType.Skip, "User Defined Status Code 917")
    },
    {
      918,
      new BAICode(918, BAICodeType.Skip, "User Defined Status Code 918")
    },
    {
      919,
      new BAICode(919, BAICodeType.Skip, "User Defined Status Code 919")
    },
    {
      920,
      new BAICode(920, BAICodeType.Skip, "User Defined Summary Credit 920")
    },
    {
      921,
      new BAICode(921, BAICodeType.Skip, "User Defined Summary Credit 921")
    },
    {
      922,
      new BAICode(922, BAICodeType.Skip, "User Defined Summary Credit 922")
    },
    {
      923,
      new BAICode(923, BAICodeType.Skip, "User Defined Summary Credit 923")
    },
    {
      924,
      new BAICode(924, BAICodeType.Skip, "User Defined Summary Credit 924")
    },
    {
      925,
      new BAICode(925, BAICodeType.Skip, "User Defined Summary Credit 925")
    },
    {
      926,
      new BAICode(926, BAICodeType.Skip, "User Defined Summary Credit 926")
    },
    {
      927,
      new BAICode(927, BAICodeType.Skip, "User Defined Summary Credit 927")
    },
    {
      928,
      new BAICode(928, BAICodeType.Skip, "User Defined Summary Credit 928")
    },
    {
      929,
      new BAICode(929, BAICodeType.Skip, "User Defined Summary Credit 929")
    },
    {
      930,
      new BAICode(930, BAICodeType.Skip, "User Defined Summary Credit 930")
    },
    {
      931,
      new BAICode(931, BAICodeType.Skip, "User Defined Summary Credit 931")
    },
    {
      932,
      new BAICode(932, BAICodeType.Skip, "User Defined Summary Credit 932")
    },
    {
      933,
      new BAICode(933, BAICodeType.Skip, "User Defined Summary Credit 933")
    },
    {
      934,
      new BAICode(934, BAICodeType.Skip, "User Defined Summary Credit 934")
    },
    {
      935,
      new BAICode(935, BAICodeType.Credit, "User Defined Detail Credit 935")
    },
    {
      936,
      new BAICode(936, BAICodeType.Credit, "User Defined Detail Credit 936")
    },
    {
      937,
      new BAICode(937, BAICodeType.Credit, "User Defined Detail Credit 937")
    },
    {
      938,
      new BAICode(938, BAICodeType.Credit, "User Defined Detail Credit 938")
    },
    {
      939,
      new BAICode(939, BAICodeType.Credit, "User Defined Detail Credit 939")
    },
    {
      940,
      new BAICode(940, BAICodeType.Credit, "User Defined Detail Credit 940")
    },
    {
      941,
      new BAICode(941, BAICodeType.Credit, "User Defined Detail Credit 941")
    },
    {
      942,
      new BAICode(942, BAICodeType.Credit, "User Defined Detail Credit 942")
    },
    {
      943,
      new BAICode(943, BAICodeType.Credit, "User Defined Detail Credit 943")
    },
    {
      944,
      new BAICode(944, BAICodeType.Credit, "User Defined Detail Credit 944")
    },
    {
      945,
      new BAICode(945, BAICodeType.Credit, "User Defined Detail Credit 945")
    },
    {
      946,
      new BAICode(946, BAICodeType.Credit, "User Defined Detail Credit 946")
    },
    {
      947,
      new BAICode(947, BAICodeType.Credit, "User Defined Detail Credit 947")
    },
    {
      948,
      new BAICode(948, BAICodeType.Credit, "User Defined Detail Credit 948")
    },
    {
      949,
      new BAICode(949, BAICodeType.Credit, "User Defined Detail Credit 949")
    },
    {
      950,
      new BAICode(950, BAICodeType.Credit, "User Defined Detail Credit 950")
    },
    {
      951,
      new BAICode(951, BAICodeType.Credit, "User Defined Detail Credit 951")
    },
    {
      952,
      new BAICode(952, BAICodeType.Credit, "User Defined Detail Credit 952")
    },
    {
      953,
      new BAICode(953, BAICodeType.Credit, "User Defined Detail Credit 953")
    },
    {
      954,
      new BAICode(954, BAICodeType.Credit, "User Defined Detail Credit 954")
    },
    {
      955,
      new BAICode(955, BAICodeType.Credit, "User Defined Detail Credit 955")
    },
    {
      956,
      new BAICode(956, BAICodeType.Credit, "User Defined Detail Credit 956")
    },
    {
      957,
      new BAICode(957, BAICodeType.Credit, "User Defined Detail Credit 957")
    },
    {
      958,
      new BAICode(958, BAICodeType.Credit, "User Defined Detail Credit 958")
    },
    {
      959,
      new BAICode(959, BAICodeType.Credit, "User Defined Detail Credit 959")
    },
    {
      960,
      new BAICode(960, BAICodeType.Skip, "User Defined Summary Debit 960")
    },
    {
      961,
      new BAICode(961, BAICodeType.Skip, "User Defined Summary Debit 961")
    },
    {
      962,
      new BAICode(962, BAICodeType.Skip, "User Defined Summary Debit 962")
    },
    {
      963,
      new BAICode(963, BAICodeType.Skip, "User Defined Summary Debit 963")
    },
    {
      964,
      new BAICode(964, BAICodeType.Skip, "User Defined Summary Debit 964")
    },
    {
      965,
      new BAICode(965, BAICodeType.Skip, "User Defined Summary Debit 965")
    },
    {
      966,
      new BAICode(966, BAICodeType.Skip, "User Defined Summary Debit 966")
    },
    {
      967,
      new BAICode(967, BAICodeType.Skip, "User Defined Summary Debit 967")
    },
    {
      968,
      new BAICode(968, BAICodeType.Skip, "User Defined Summary Debit 968")
    },
    {
      969,
      new BAICode(969, BAICodeType.Skip, "User Defined Summary Debit 969")
    },
    {
      970,
      new BAICode(970, BAICodeType.Skip, "User Defined Summary Debit 970")
    },
    {
      971,
      new BAICode(971, BAICodeType.Skip, "User Defined Summary Debit 971")
    },
    {
      972,
      new BAICode(972, BAICodeType.Skip, "User Defined Summary Debit 972")
    },
    {
      973,
      new BAICode(973, BAICodeType.Skip, "User Defined Summary Debit 973")
    },
    {
      974,
      new BAICode(974, BAICodeType.Skip, "User Defined Summary Debit 974")
    },
    {
      975,
      new BAICode(975, BAICodeType.Debit, "User Defined Detail Debit 975")
    },
    {
      976,
      new BAICode(976, BAICodeType.Debit, "User Defined Detail Debit 976")
    },
    {
      977,
      new BAICode(977, BAICodeType.Debit, "User Defined Detail Debit 977")
    },
    {
      978,
      new BAICode(978, BAICodeType.Debit, "User Defined Detail Debit 978")
    },
    {
      979,
      new BAICode(979, BAICodeType.Debit, "User Defined Detail Debit 979")
    },
    {
      980,
      new BAICode(980, BAICodeType.Debit, "User Defined Detail Debit 980")
    },
    {
      981,
      new BAICode(981, BAICodeType.Debit, "User Defined Detail Debit 981")
    },
    {
      982,
      new BAICode(982, BAICodeType.Debit, "User Defined Detail Debit 982")
    },
    {
      983,
      new BAICode(983, BAICodeType.Debit, "User Defined Detail Debit 983")
    },
    {
      984,
      new BAICode(984, BAICodeType.Debit, "User Defined Detail Debit 984")
    },
    {
      985,
      new BAICode(985, BAICodeType.Debit, "User Defined Detail Debit 985")
    },
    {
      986,
      new BAICode(986, BAICodeType.Debit, "User Defined Detail Debit 986")
    },
    {
      987,
      new BAICode(987, BAICodeType.Debit, "User Defined Detail Debit 987")
    },
    {
      988,
      new BAICode(988, BAICodeType.Debit, "User Defined Detail Debit 988")
    },
    {
      989,
      new BAICode(989, BAICodeType.Debit, "User Defined Detail Debit 989")
    },
    {
      990,
      new BAICode(990, BAICodeType.Debit, "User Defined Detail Debit 990")
    },
    {
      991,
      new BAICode(991, BAICodeType.Debit, "User Defined Detail Debit 991")
    },
    {
      992,
      new BAICode(992, BAICodeType.Debit, "User Defined Detail Debit 992")
    },
    {
      993,
      new BAICode(993, BAICodeType.Debit, "User Defined Detail Debit 993")
    },
    {
      994,
      new BAICode(994, BAICodeType.Debit, "User Defined Detail Debit 994")
    },
    {
      995,
      new BAICode(995, BAICodeType.Debit, "User Defined Detail Debit 995")
    },
    {
      996,
      new BAICode(996, BAICodeType.Debit, "User Defined Detail Debit 996")
    },
    {
      997,
      new BAICode(997, BAICodeType.Debit, "User Defined Detail Debit 997")
    },
    {
      998,
      new BAICode(998, BAICodeType.Debit, "User Defined Detail Debit 998")
    },
    {
      999,
      new BAICode(999, BAICodeType.Debit, "User Defined Detail Debit 999")
    }
  };
}
