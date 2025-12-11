// Selectors.
var _cartModelSelector = '#cart-model';
var _itemQuantitySelector = '#bx--cartitem-quantity-';
var _itemTotalPriceSelector = '#bx--cartitem-totalprice-';
var _totalPriceSelector = '#bx--cart-totalprice';
var _totalItemsSelector = '#bx--cart-totalitems';
var _cartItemSelector = '#bx--cart-item-';
var _cartItemsSelector = '.bx--cart-items';
var _cartCheckoutSelector = '.bx--cart-checkout';
var _cartNoRecordsSelector = '.bx--cart-no-records';
var _notificationPlaceholderSelector = '#notification';
var _notificationSelector = '.bx--inline-notification--success';
var _notificationErrorSelector = '.bx--inline-notification--error';
var _badgeSelector = '.bx--header__action.bx--badge-parent span';

// Controller Action Methods
var _updateCart = '/Cart/UpdateCart';
var _successNotification = '/Product/SuccessNotification';
var _errorNotification = '/Product/ErrorNotification';

// Messages
var _cartDeleteMessage = "Item Deleted Successfully";
var _cartUpdateMessage = "Cart Updated Successfully";

function getValue(selector) { return $(selector).val() }
function setValue(selector, value) { return $(selector).val(value) }
function setText(selector, value) { return $(selector).text(value) }
function deleteCartItem(cartItemId, index) { event.preventDefault(); this.deleteItem(cartItemId, index); }

function deleteItem(cartItemId, index) {
    var cartModel = JSON.parse(this.getValue(_cartModelSelector));
    cartModel.Items = $.grep(cartModel.Items, function (e) { return e.ProductId != cartItemId });
    var totalPrice = 0;
    $.each(cartModel.Items, function () { totalPrice += parseFloat(this.UnitPrice) * parseInt(this.Quantity); });
    cartModel.TotalPrice = totalPrice;
    cartModel.TotalItems = cartModel.Items.length;
    this.setValue(_cartModelSelector, JSON.stringify(cartModel));
    this.setText(_totalItemsSelector, cartModel.TotalItems);
    this.setText(_totalPriceSelector, cartModel.TotalPrice);
    this.updateCartAjax(cartModel, _cartDeleteMessage);
    this.removeCartItem(index, cartModel.TotalItems);
}

function removeCartItem(index, totalItems) {
    $(_cartItemSelector + index).remove();
    if (totalItems == 0) {
        $(_cartItemsSelector).hide();
        $(_cartCheckoutSelector).hide();
        $(_cartNoRecordsSelector).show();
    }
}

function updateItemQuantity(index, cartItemId, direction) {
    var quantity = parseInt(getValue(_itemQuantitySelector + index));
    if (direction) { quantity += 1; } else if (quantity == 1) return; else { quantity -= 1; }
    this.setValue(_itemQuantitySelector + index, quantity)
    var cartModel = JSON.parse(this.getValue(_cartModelSelector));
    var currentItemPrice = 0; var totalPrice = 0;
    $.each(cartModel.Items, function () { if (this.ProductId == cartItemId) { this.Quantity = quantity; currentItemPrice = this.UnitPrice } });
    $.each(cartModel.Items, function () { totalPrice += parseFloat(this.UnitPrice) * parseInt(this.Quantity); });
    cartModel.TotalPrice = totalPrice;
    this.setValue(_cartModelSelector, JSON.stringify(cartModel));
    this.setText(_itemTotalPriceSelector + index, (currentItemPrice * quantity).toFixed(2));
    this.setText(_totalItemsSelector, cartModel.TotalItems);
    this.setText(_totalPriceSelector, cartModel.TotalPrice);
}

function saveCartItem() {
    var cartModel = JSON.parse(this.getValue(_cartModelSelector));
    this.updateCartAjax(cartModel, _cartUpdateMessage);
}

function updateCartAjax(cartModel, message) {
    $(_notificationErrorSelector).remove();
    $.ajax({
        url: _updateCart,
        data: cartModel,
        type: 'POST',
        success: function (result) {
            $(_badgeSelector).html(result.cartCount);
            ShowSuccessNotificationAjax(message)
        }
    });
}

function ShowSuccessNotificationAjax(message) {
    $.ajax({
        url: _successNotification,
        type: "POST",
        data: { message: message },
        success: function (data) {
            if (data) {
                $(_notificationPlaceholderSelector).html(data);
                setInterval(function () { $(_notificationSelector).remove(); }, 5000);
            }
        }
    });
}

function ShowErrorNotificationAjax(message) {
    $.ajax({
        url: _errorNotification,
        type: "POST",
        data: { message: message },
        success: function (data) {
            if (data) {
                $(_notificationPlaceholderSelector).html(data);
                setInterval(function () { $(_notificationErrorSelector).remove(); }, 5000);
            }
        }
    });
}

$(document).on('click', _notificationSelector, function (e) { this.remove(); });

$(document).on('click', _notificationErrorSelector, function (e) { this.remove(); });