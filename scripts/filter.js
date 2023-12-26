var script = document.createElement('script');
script.src = 'https://code.jquery.com/jquery-3.6.0.min.js';
document.getElementsByTagName('head')[0].appendChild(script);
script.addEventListener('load', function () {
    $(document).ready(init);
});

function init() {
    applyHTML();
    applyCSS()
    applyFilter();

    function applyHTML() {
        $("body").prepend("<div class=\"filter\" id=\"filter\"></div>");
        $(".filter").append("<div class=\"filter-buttons\" id=\"filter-buttons\"></div>");
        $("<button type=\"button\" class=\"btn all\" id=\"all\">ALL</button><br>").appendTo("#filter");
        $("<button type=\"button\" class=\"btn action\" id=\"action\">ACTION</button><br>").appendTo("#filter");
        $("<button type=\"button\" class=\"btn adventure\" id=\"adventure\">ADVENTURE</button><br>").appendTo("#filter");
        $("<button type=\"button\" class=\"btn biography\" id=\"biography\">BIOGRAPHY</button><br>").appendTo("#filter");
        $("<button type=\"button\" class=\"btn comedy\" id=\"comedy\">COMEDY</button><br>").appendTo("#filter");
        $("<button type=\"button\" class=\"btn crime\" id=\"crime\">CRIME</button><br>").appendTo("#filter");
        $("<button type=\"button\" class=\"btn drama\" id=\"drama\">DRAMA</button><br>").appendTo("#filter");
        $("<button type=\"button\" class=\"btn horror\" id=\"horror\">HORROR</button><br>").appendTo("#filter");
        $("<button type=\"button\" class=\"btn Romantic\" id=\"romantic\">ROMANTIC</button><br>").appendTo("#filter");
    }
    
    function applyCSS() {
        $(".filter").css(  
            {   
                "display": "flex",
                "justify-content": "space-between",
                "margin-top": "100px"
            }
        );

        $(".button").css(  
            {   
                "margin-right": "0px"
            }
        );
    }

    function applyFilter() {
        var $secondColumnCells = $('.table tr td:nth-child(2)');

        $(document).ready(function () {
            var $btns = $('.btn').click(function () {
                if (this.id == 'all') {
                    $('.table > tbody > tr').fadeIn(450);
                } else {
                    var genre = this.id.toLowerCase();
                    var $el = $('.table > tbody > tr').filter(function () {
                        return $(this).find('td:nth-child(2)').text().trim().toLowerCase().localeCompare(genre, undefined, { sensitivity: 'base' }) === 0;
                    }).fadeIn(450);

                    $('.table > tbody > tr').not($el).hide();
                }

                $btns.removeClass('active');
                $(this).addClass('active');
            });

            var $movies = $('.table > tbody > tr');

            $btns.first().trigger('click');
        });
    }
    
}