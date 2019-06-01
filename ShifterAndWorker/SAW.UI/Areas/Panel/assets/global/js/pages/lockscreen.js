$(function() {
    $.backstretch(["~/Areas/Panel/assets/global/images/gallery/login4.jpg", "~/Areas/Panel/assets/global/images/gallery/login3.jpg", "~/Areas/Panel/assets/global/images/gallery/login2.jpg", "~/Areas/Panel/assets/global/images/gallery/login.jpg"], {
        fade: 600,
        duration: 4000
    });
    /* Creation of the Circle Progress */
    var circle = new ProgressBar.Circle('#loader', {
        color: '#aaa',
        strokeWidth: 3,
        trailWidth: 3,
        trailColor: 'rgba(255,255,255,0.1)',
        easing: 'easeInOut',
        duration: 2000,
        from: {
            color: '#319DB5',
            width: 3
        },
        to: {
            color: '#319DB5',
            width: 3
        },
        // Set default step function for all animate calls
        step: function(state, circle) {
            circle.path.setAttribute('stroke', state.color);
            circle.path.setAttribute('stroke-width', state.width);
        }
    });

    //$('.btn-primary').click(function(e) {
    //    e.preventDefault();
    //    circle.animate(1);
    //    setTimeout(function() {
    //        $('.loader-overlay').removeClass('loaded').fadeIn(150);
    //        //setTimeout(function() {
    //        //    window.location = "dashboard.html";
    //        //}, 1000);
    //    }, 2000);
    //});


    
    /* Background Slide */
    $('.bg-slider').backstretch(["~/Areas/Panel/assets/global/images/gallery/login4.jpg", "~/Areas/Panel/assets/global/images/gallery/login3.jpg", "~/Areas/Panel/assets/global/images/gallery/login2.jpg", "~/Areas/Panel/assets/global/images/gallery/login.jpg"], {
        fade: 600,
        duration: 4000
    });

    /* Show / Hide Builder on Mouseover */
    $("#account-builder").on('mouseenter', function() {
        TweenMax.to($(this), 0.35, {
            css: {
                height: 125,
                width: 500,
                marginLeft: -250
            },
            ease: Circ.easeInOut
        });
    });

    $("#account-builder").on('mouseleave', function() {
        TweenMax.to($(this), 0.35, {
            css: {
                height: 44,
                width: 250,
                marginLeft: -125
            },
            ease: Circ.easeInOut
        });
    });

    /* Hide / Show Background Image */
    $('#image-cb').change(function() {
        if ($(this).is(":checked")) {
            $.backstretch(["~/Areas/Panel/assets/global/images/gallery/login.jpg"], {
                fade: 600,
                duration: 4000
            });
            $('#slide-cb').attr('checked', false);
        }
        else $.backstretch("destroy");
    });

    /* Add / Remove Slide Image */
    $('#slide-cb').change(function() {
        if ($(this).is(":checked")) {
            $.backstretch(["~/Areas/Panel/assets/global/images/gallery/login4.jpg", "~/Areas/Panel/assets/global/images/gallery/login3.jpg", "~/Areas/Panel/assets/global/images/gallery/login2.jpg", "~/Areas/Panel/assets/global/images/gallery/login.jpg"], {
                fade: 600,
                duration: 4000
            });
            $('#image-cb').attr('checked', false);
        }
        else {
            $.backstretch("destroy");
        }
    });

    /* Separate Inputs */
    $('#input-cb').change(function() {
        if ($(this).is(":checked")) {
            TweenMax.to($('.input-group .btn'), 0.3, {
                css: {
                    borderRadius: 0
                },
                ease: Circ.easeInOut
            });
            TweenMax.to($('.input-group #password'), 0.3, {
                css: {
                    borderRadius: 0
                },
                ease: Circ.easeInOut,
                onComplete: function() {
                    TweenMax.to($('.input-group'), 0.3, {
                        css: {
                            marginLeft: '-10px',
                            'border-spacing': '10px 0'
                        },
                        ease: Circ.easeInOut,
                        onComplete: function() {
                            $('body').addClass('separate-inputs');
                        }
                    });
                }
            });
        }
        else {
            TweenMax.to($('.input-group .btn'), 0.3, {
                css: {
                    borderRadius: '0 17px 17px 0'
                },
                ease: Circ.easeInOut
            });
            TweenMax.to($('.input-group #password'), 0.3, {
                css: {
                    borderRadius: '17px 0 0 17px'
                },
                ease: Circ.easeInOut,
                onComplete: function() {
                    TweenMax.to($('.input-group'), 0.3, {
                        css: {
                            marginLeft: 0,
                            'border-spacing': '0'
                        },
                        ease: Circ.easeInOut,
                        onComplete: function() {
                            $('body').removeClass('separate-inputs');
                        }
                    });
                }
            });
        }
    });

    /* Hide / Show User Image */
    $('#user-cb').change(function() {
        if ($(this).is(":checked")) {
            TweenMax.to($('.user-image'), 0.3, {
                opacity: 1,
                ease: Circ.easeInOut
            });
            TweenMax.to($('.form-signin'), 0.3, {
                marginLeft: 0,
                ease: Circ.easeInOut
            });
        }
        else {
            TweenMax.to($('.user-image'), 0.3, {
                opacity: 0,
                ease: Circ.easeInOut
            });
            TweenMax.to($('.form-signin'), 0.3, {
                marginLeft: -50,
                ease: Circ.easeInOut
            });
        }
    });
});