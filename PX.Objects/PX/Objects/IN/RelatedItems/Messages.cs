// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.Messages
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.IN.RelatedItems;

[PXLocalizable]
public static class Messages
{
  public const string RelatedItemsFilter = "Related Items Filter";
  public const string RelatedItem = "Related Item";
  public const string INRelatedInventoryUserFeedback = "Related Item ML Feedback";
  public const string RelatedItemsHistoryFilter = "Related Items History Filter";
  public const string RelatedItemHistory = "Related Item History";
  public const string RelatedItemsField = "Related Items";
  public const string UsingInventoryAsItsRelated = "The inventory item cannot be selected as its own related item. Select another item.";
  public const string RelatedItemAlreadyExists = "A line with the {0} relation and the {1} UOM already exists for {2} for the specified date range. Remove the line or change its details.";
  public const string DuplicateRelatedItems = "Changes cannot be saved. View the Related Items tab for details.";
  public const string RelatedItemsAvailable = "{0} items exist for this item. Click this button to select an item.";
  public const string RelatedItemsAvailable2 = "{0} and {1} items exist for this item. Click this button to select an item.";
  public const string RelatedItemsAvailable3 = "{0}, {1}, and {2} items exist for this item. Click this button to select an item.";
  public const string RelatedItemsAvailable4 = "{0}, {1}, {2}, and {3} items exist for this item. Click this button to select an item.";
  public const string RelatedItemsRequired = "Related items are required for this item. Click this button to select a related item.";
  public const string SubstituteItemsRequired = "This item has to be substituted. Click this button to select a substitute item.";
  public const string QuantityIsNotSufficient = "The quantity of {0} in the {1} warehouse is not sufficient to fulfill the order. {2} items are available for this item. Click the button in the Related Items column to select an item.";
  public const string QuantityIsNotSufficient2 = "The quantity of {0} in the {1} warehouse is not sufficient to fulfill the order. {2} and {3} items are available for this item. Click the button in the Related Items column to select an item.";
  public const string QuantityIsNotSufficient3 = "The quantity of {0} in the {1} warehouse is not sufficient to fulfill the order. {2}, {3}, and {4} items are available for this item. Click the button in the Related Items column to select an item.";
  public const string QuantityIsNotSufficient4 = "The quantity of {0} in the {1} warehouse is not sufficient to fulfill the order. {2}, {3}, {4}, and {5} items are available for this item. Click the button in the Related Items column to select an item.";
  public const string AvailableQtyIsLessThanSelected = "The available quantity of the item is less than the selected quantity.";
  public const string LineContainsRequiredRelatedItem = "The line contains the item that requires substitution. Select a substitute item by using the button in the Related Items column.";
  public const string ShipmentCannotBeCreated = "The shipment cannot be created because it contains items that require substitution. Select substitute items by using the buttons in the Related Items column of the Details tab.";
  public const string InvoiceCannotBeReleased = "The invoice cannot be released because it contains items that require substitution. Select substitute items by using the buttons in the Related Items column of the Details tab.";
  public const string ShipmentCannotBeCreatedOnProcessingScreen = "The shipment cannot be created because it contains items that require substitution. Select substitute items by using the buttons in the Related Items column of the Details tab on the Sales Orders (SO301000) form.";
  public const string InvoiceCannotBeReleasedOnProcessingScreen = "The invoice cannot be released because it contains items that require substitution. Select substitute items by using the buttons in the Related Items column of the Details tab on the Invoices (SO303000) form.";
  public const string ApproveCrossSellSuggestion = "Approve";
  public const string DeleteCrossSellSuggestion = "Delete";
  public const string SenderEmailNotSet = "The From box is empty in the {0} email template on the Email Templates (SM204003) form.";
  public const string ReceiverEmailNotSet = "The To box is empty in the {0} email template on the Email Templates (SM204003) form.";
  public const string EmailNotSent = "Failed to send the email.";
}
