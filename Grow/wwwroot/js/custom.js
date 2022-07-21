$(window).scroll(function () {

    var page = $(window),
        cont = $('body'),
        div = $('.change-color'),
        grow = $('.grow'),
        lr = $('.left-right');

    var scroll = page.scrollTop() + (page.height() / 2);
    var rounded = Math.floor(scroll);
    var str_fontsize = rounded.toString();
    if (str_fontsize.length == 3) {
        str_fontsize = str_fontsize.substr(0, 2)
    }
    else if (str_fontsize.length == 4) {
        str_fontsize = str_fontsize.substr(0, 3)
    }
    var num_fontsize = parseInt(str_fontsize);
    var num__fontsize = 40 + num_fontsize;
    var lr_size = ($(window).width() * (-1)) + scroll;
    console.log(lr_size);
    div.each(function () {
        var $this = $(this);
        if ($this.position().top <= scroll && $this.position().top + $this.height() > scroll) {

            cont.removeClass(function (index, css) {
                return (css.match(/(^|\s)color-\S+/g) || []).join(' ');
            });
            cont.addClass('color-' + $(this).data('color'));
            grow.css("font-size", num__fontsize);
            lr.css("left", lr_size)
        }
    });

}).scroll();