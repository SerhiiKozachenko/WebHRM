(function() {

  $(function() {
    $('span.field-validation-valid, span.field-validation-error').each(function() {
      return $(this).addClass('help-inline');
    });
    $('form').submit(function() {
      if ($(this).valid()) {
        return $(this).find('div.control-group').each(function() {
          if ($(this).find('span.field-validation-error').length === 0) {
            return $(this).removeClass('error');
          }
        });
      } else {
        return $(this).find('div.control-group').each(function() {
          if ($(this).find('span.field-validation-error').length > 0) {
            return $(this).addClass('error');
          }
        });
      }
    });
    return $('form').each(function() {
      return $(this).find('div.control-group').each(function() {
        if ($(this).find('span.field-validation-error').length > 0) {
          return $(this).addClass('error');
        }
      });
    });
  });

}).call(this);
