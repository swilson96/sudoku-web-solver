(function($) {
    $("button").click(function() {
        $("input").each(function() {
            var input = $(this);
            if (input.val()) {
                input.addClass("bold");
            } else {
                input.removeClass("bold");
            }
        });
    });
})(jQuery);