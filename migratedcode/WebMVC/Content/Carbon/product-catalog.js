// Selectors.
var _brandSelector = "input[type='radio'][name='prodcutBrand']:checked";
var _typeSelector = "input[type='radio'][name='productType']:checked";
var _productListSelector = '#productList';
var _notificationSelector = '.bx--inline-notification--success';
var _notificationErrorSelector = '.bx--inline-notification--error';
var _notificationPlaceholderSelector = '#notification';
var _badgeSelector = '.bx--header__action.bx--badge-parent span';

// Controller Action Methods
var _productSearch = '/Product/ProductFilter';
var _addCart = '/Product/AddToCart';
var _successNotification = '/Product/SuccessNotification';
var _errorNotification = '/Product/ErrorNotification';

// Messages
var _productAddToCartMessage = "Product Added to cart successfully";
var _productAddToCartErrorMessage = 'Product was not added to cart. Please try again'

function SearchProduct() {
    var brandId = $(_brandSelector).attr('value');
    var typeId = $(_typeSelector).attr('value');
    productSearchAjax(brandId, typeId);
}

function productSearchAjax(brandId, typeId) {
    $(_notificationErrorSelector).remove();
    $.ajax({
        url: _productSearch,
        data: { brandId: brandId, typeId: typeId },
        type: 'POST',
        success: function (result) {
            $(_productListSelector).html(result);
        }
    });
}

$(document).on('click', _notificationSelector, function (e) { this.remove(); });

$(document).on('click', _notificationErrorSelector, function (e) { this.remove(); });

function AddToCart(productId) {
    $.ajax({
        url: _addCart,
        type: "POST",
        data: { productId: productId },
        success: function (data) {
            if (data.isSuccessfull) {
                $(_badgeSelector).html(data.cartCount);
                ShowSuccessNotification(_productAddToCartMessage);
            } else {
                ShowErrorNotification(_productAddToCartErrorMessage);
            }
        }
    });
}

function ShowSuccessNotification(message) {
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

function ShowErrorNotification(message) {
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