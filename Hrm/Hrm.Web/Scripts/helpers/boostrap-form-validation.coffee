$ ->
	$('span.field-validation-valid, span.field-validation-error').each ()->
		$(this).addClass('help-inline')

	$('form').submit ()->
		if $(this).valid()
			$(this).find('div.control-group').each ()->
				if $(this).find('span.field-validation-error').length == 0
					$(this).removeClass('error')

		else
			$(this).find('div.control-group').each ()->
				if $(this).find('span.field-validation-error').length > 0
					$(this).addClass('error')

	$('form').each ()->
		$(this).find('div.control-group').each ()->
			if $(this).find('span.field-validation-error').length > 0
				$(this).addClass('error')
		
			