jQuery(function($) {

  "use strict";


  /**
   * introLoader - Preloader
   */
  $("#introLoader").introLoader({
    animation: {
      name: 'gifLoader',
      options: {
        ease: "easeInOutCirc",
        style: 'dark bubble',
        delayBefore: 500,
        delayAfter: 0,
        exitTime: 300
      }
    }
  });


  /**
   * Sticky Header
   */
  $(".container-wrapper").waypoint(function() {
    $(".navbar").toggleClass("navbar-sticky");
    return false;
  }, { offset: "-20px" });



  /**
   * Main Menu Slide Down Effect
   */

  // Mouse-enter dropdown
  $('#navbar li').on("mouseenter", function() {
    $(this).find('ul').first().stop(true, true).delay(350).slideDown(500, 'easeInOutQuad');
  });

  // Mouse-leave dropdown
  $('#navbar li').on("mouseleave", function() {
    $(this).find('ul').first().stop(true, true).delay(100).slideUp(150, 'easeInOutQuad');
  });



  $('.collapse.in').prev('.panel-heading').addClass('active');
  $('.bootstarp-accordion, .bootstarp-accordion')
    .on('show.bs.collapse', function(a) {
      $(a.target).prev('.panel-heading').addClass('active');
    })
    .on('hide.bs.collapse', function(a) {
      $(a.target).prev('.panel-heading').removeClass('active');
    });


  /**
   * Slicknav - a Mobile Menu
   */
  var $slicknav_label;
  $('#responsive-menu').slicknav({
    duration: 500,
    easingOpen: 'easeInExpo',
    easingClose: 'easeOutExpo',
    closedSymbol: '<i class="fa fa-plus"></i>',
    openedSymbol: '<i class="fa fa-minus"></i>',
    prependTo: '#slicknav-mobile',
    allowParentLinks: true,
    label: "",
  });


  /**
   * Smooth scroll to anchor
   */
  $('a.anchor[href*=#]:not([href=#])').on("click", function() {
    if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
      var target = $(this.hash);
      target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
      if (target.length) {
        $('html,body').animate({
          scrollTop: (target.offset().top - 90) // 90 offset for navbar menu
        }, 1000);
        return false;
      }
    }
  });

  $('a.anchor-alt').on("click", function() {
    if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
      var target = $(this.hash);
      target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
      if (target.length) {
        $('html,body').animate({
          scrollTop: (target.offset().top - 130)
        }, 1000);
        return false;
      }
    }
  });


  /**
   * Effect to Bootstrap Dropdown
   */
  $('.bt-dropdown-click').on('show.bs.dropdown', function(e) {
    $(this).find('.dropdown-menu').first().stop(true, true).slideDown(500, 'easeInOutQuad');
  });
  $('.bt-dropdown-click').on('hide.bs.dropdown', function(e) {
    $(this).find('.dropdown-menu').first().stop(true, true).slideUp(250, 'easeInOutQuad');
  });



  /**
   * Bootstrap rating
   */

  $('.rating-label').rating();

  $('.rating-label').each(function() {
    $('<span class="label label-default"></span>')
      .text($(this).val() || ' ')
      .insertAfter(this);
  });
  $('.rating-label').on('change', function() {
    $(this).next('.label').text($(this).val());
  });


  /**
   * Another Bootstrap Toggle
   */
  $('.another-toggle').each(function() {
    if ($('h4', this).hasClass('active')) {
      $(this).find('.another-toggle-content').show();
    }
  });
  $('.another-toggle h4').on("click", function() {
    if ($(this).hasClass('active')) {
      $(this).removeClass('active');
      $(this).next('.another-toggle-content').slideUp();
    } else {
      $(this).addClass('active');
      $(this).next('.another-toggle-content').slideDown();
    }
  });


  /**
   *  Arrow for Menu has sub-menu
   */
  /* if ($(window).width() > 992) {
  	$(".navbar-arrow > ul > li").has("ul").children("a").append("<i class='arrow-indicator fa fa-angle-down'></i>");
  } */

  if ($(window).width() > 992) {
    $(".navbar-arrow ul ul > li").has("ul").children("a").append("<i class='arrow-indicator fa fa-angle-right'></i>");
  }


  /**
   * Back To Top
   */
  $(window).scroll(function() {
    if ($(window).scrollTop() > 500) {
      $("#back-to-top").fadeIn(200);
    } else {
      $("#back-to-top").fadeOut(200);
    }
  });
  $('#back-to-top').on("click", function() {
    $('html, body').animate({ scrollTop: 0 }, '800');
    return false;
  });



  /**
   * Select 2 - Custom select
   */
  $(".select2-single").select2({ allowClear: true });
  $(".select2-single-no-search").select2({ allowClear: false, minimumResultsForSearch: Infinity });
  $(".select2-multi").select2({ placeholder: "Select a state", });
  $(".select2-category-select").select2({ placeholder: "All category", });



  /**
   * ion Range Slider
   */
  $("#price_range").ionRangeSlider({
    grid: true,
    type: "double",
    min: 0,
    max: 3000,
    from: 4,
    to: 800,
    prefix: "$",
  });
  $("#star_rating_range").ionRangeSlider({
    type: "double",
    min: 1,
    max: 5,
    step: 1,
    grid: true,
    grid_snap: true,
  });

  $('.btn-more-less').on("click", function() {
    $(this).text(function(i, old) {
      return old == 'Show more' ? 'Show less' : 'Show more';
    });
  });



  /**
   *  Placeholder
   */
  $("input, textarea").placeholder();



  /**
   * Bootstrap tooltips
   */
  $('[data-toggle="tooltip"]').tooltip();



  /**
   * Fancy - Custom Select
   */
  $('.custom-select').fancySelect(); // Custom select



  /**
   * Instagram
   */
  function createPhotoElement(photo) {
    var innerHtml = $('<img>')
      .addClass('instagram-image')
      .attr('src', photo.images.thumbnail.url);

    innerHtml = $('<a>')
      .attr('target', '_blank')
      .attr('href', photo.link)
      .append(innerHtml);

    return $('<div>')
      .addClass('instagram-placeholder')
      .attr('id', photo.id)
      .append(innerHtml);
  }

  function didLoadInstagram(event, response) {
    var that = this;
    $.each(response.data, function(i, photo) {
      $(that).append(createPhotoElement(photo));
    });
  }

  $('#instagram').on('didLoadInstagram', didLoadInstagram);
  $('#instagram').instagram({
    count: 15,
    userId: 3301700665,
    accessToken: '3301700665.4445ec5.c3ba39ad7828412286c1563cac3f594b'
  });

  $('#instagram_long').on('didLoadInstagram', didLoadInstagram);
  $('#instagram_long').instagram({
    count: 8,
    userId: 3301700665,
    accessToken: '3301700665.4445ec5.c3ba39ad7828412286c1563cac3f594b'
  });



  /**
   * Sign-in and Sign-up modal
   */

  var $modal = $('#ajaxLoginModal');

  $(document).on('click', '.btn-ajax-login,.login-box-box-action a', function() {
    // create the backdrop and wait for next modal to be triggered

    $modalForgotPassword.modal('hide');
    $modalRegister.modal('hide');

    $('body').modalmanager('loading');

    setTimeout(function() {
      $modal.load('ajax-login-modal-login.html', '', function() {
        $modal.modal();
      });
    }, 1000);
  });

  /** for Register Ajax Modal */

  var $modalRegister = $('#ajaxRegisterModal');

  $(document).on('click', '.btn-ajax-register,.register-box-box-action a', function() {

    $modal.modal('hide');
    $modalForgotPassword.modal('hide');

    $('body').modalmanager('loading');

    setTimeout(function() {
      $modalRegister.load('ajax-login-modal-register.html', '', function() {
        $modalRegister.modal();
      });
    }, 1000);
  });

  /** for Forgot Password Ajax Modal */

  var $modalForgotPassword = $('#ajaxForgotPasswordModal');

  $(document).on('click', '.btn-ajax-forgot-password,.login-box-link-action a', function() {

    $modal.modal('hide');
    $modalRegister.modal('hide');

    $('body').modalmanager('loading');

    setTimeout(function() {

      $modalForgotPassword.load('ajax-login-modal-forgot-password.html', '', function() {
        $modalForgotPassword.modal();
      });

    }, 1000);

  });


  // JS init calling
  initSlider();


});

/**
 * Slick Carousel and Slider
 */
function initSlider() {

  $('.slick-testimonial-item').slick({
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
    autoplay: true,
    arrows: false,
    autoplaySpeed: 5000,
  });

  $('.slick-featured-restuarant-item').slick({
    dots: false,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
    autoplay: true,
    arrows: true,
    autoplaySpeed: 5000,
  });

  $('.slick-hero-slider').slick({
    dots: false,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
    centerMode: true,
    infinite: true,
    centerPadding: '0',
    focusOnSelect: true,
    adaptiveHeight: true,
    autoplay: true,
    autoplaySpeed: 4500,
    pauseOnHover: true,
  });

/**
 * User edit text-box
 */

	$('body').on('focus', '[contenteditable]', function(event) {
    if (!$(this).hasClass('active')) {
      $(this).addClass('active');
      let id = $(this).attr('dataId');
      $(this).wrapInner("<div class='text-edit' name='"+ id +"' id='" + id + "'></div>");
      initCKEditor(id);
      $(this).append($('<button class="btn btn-primary btn-form user-edit-button">Save</button>'));
    }
		// $.each(arrayChangeText, function(index, el) {
		// 	this.append($('<textarea class="user-edit-textarea"></textarea>'));
		// 	this.append($('<button class="btn btn-primary btn-form user-edit-button">Edit</button>'));
		// });
		// arrayChangeText.wrap($('<textarea class="user-edit-textarea"></textarea>'));
	});
	$('body').on('blur', '[contenteditable]', function (event) {
	    $(".user-edit-button").remove();
	    var cnt = $(".text-edit").contents();
	    $(".text-edit").replaceWith(cnt);
	    $(this).removeClass('active');
	});

  const URL_DOMAIN = window.location.origin;
  const localizableStrings = '/api/localizablestrings/update/';

  $('body').on('mousedown', '.user-edit-button', function(event) {
    let dom = $(this).parent();

    dom.find('.text-edit').contents().unwrap();
    dom.find('.user-edit-button').remove();
    dom.removeAttr('class aria-label title tabindex spellcheck role');

    $.ajax({
      url: URL_DOMAIN + localizableStrings,
      datatype: 'json',
      type: "post",
      contentType: "application/json",
      data:JSON.stringify({
        id: +dom.attr('dataId'),
        item: dom[0].outerHTML
      }),
      success: function(data) {
        console.log(data.successed);
      }
    });
  });

  const loadmoreplaces = '/api/loadmoreplaces/';

  $("body").on('click', "#loadmoreplaces_btn", function () {    
      let loadedPlaces = [];
      var elements = $(".restaurant-grid-item");
      for (let i = 0; i < elements.length; i++) {
          loadedPlaces.push($(elements[i]).attr("dataid"));
      }
      $.ajax({
          url: URL_DOMAIN + loadmoreplaces,
          datatype: 'json',
          type: "post",
          contentType: "application/json",
          data: JSON.stringify({
               loadedPlacesIds: loadedPlaces
          }),
          success: function(dom) {
              $(".c-this-city-places").replaceWith(dom);
          }
      });
  });


  /*
  * CKEditor config
  */

  CKEDITOR.config.height = 150;
  CKEDITOR.config.width = 'auto';

  var initCKEditor = function(id) {
    var wysiwygareaAvailable = isWysiwygareaAvailable(),
      isBBCodeBuiltIn = !!CKEDITOR.plugins.get( 'bbcode' );

    return function(id) {
      var editorElement = CKEDITOR.document.getById(id);

      // :(((
      if ( isBBCodeBuiltIn ) {
        editorElement.setHtml(
          'Hello world!\n\n' +
          'I\'m an instance of [url=http://ckeditor.com]CKEditor[/url].'
        );
      }

      // Depending on the wysiwygare plugin availability initialize classic or inline editor.
      if ( wysiwygareaAvailable ) {
        CKEDITOR.replace(id);
      } else {
        editorElement.setAttribute( 'contenteditable', 'true' );
        CKEDITOR.inline(id);

        // TODO we can consider displaying some info box that
        // without wysiwygarea the classic editor may not work.
      }
    };

    function isWysiwygareaAvailable() {
      // If in development mode, then the wysiwygarea must be available.
      // Split REV into two strings so builder does not replace it :D.
      if ( CKEDITOR.revision == ( '%RE' + 'V%' ) ) {
        return true;
      }

      return !!CKEDITOR.plugins.get( 'wysiwygarea' );
    }
  };
}