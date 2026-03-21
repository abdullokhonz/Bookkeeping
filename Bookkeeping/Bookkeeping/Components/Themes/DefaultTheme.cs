using MudBlazor;

namespace Bookkeeping.Components.Themes
{
    public static class DefaultTheme
    {
        public static readonly MudTheme Theme = new()
        {
            PaletteLight = new PaletteLight()
            {
                PrimaryContrastText = "rgba(255,255,255,1)",
                SecondaryContrastText = "rgba(255,255,255,1)",
                SecondaryDarken = "rgb(255,31,105)",
                SecondaryLighten = "rgb(255,102,153)",
                Secondary = "rgba(255,64,129,1)",
                TertiaryContrastText = "rgba(255,255,255,1)",
                TertiaryLighten = "rgb(42,223,187)",
                Tertiary = "rgba(30,200,165,1)",
                TertiaryDarken = "rgb(25,169,140)",
                InfoContrastText = "rgba(255,255,255,1)",
                SuccessContrastText = "rgba(255,255,255,1)",
                WarningContrastText = "rgba(255,255,255,1)",
                ErrorContrastText = "rgba(255,255,255,1)",
                DarkContrastText = "rgba(255,255,255,1)",
                GrayDark = "#757575",
                White = "rgba(255,255,255,1)",
                OverlayLight = "rgba(255,255,255,0.4980392156862745)",
                OverlayDark = "rgba(33,33,33,0.4980392156862745)",
                GrayDarker = "#616161",
                TableHover = "rgba(0,0,0,0.0392156862745098)",
                GrayLighter = "#E0E0E0",
                GrayLight = "#BDBDBD",
                GrayDefault = "#9E9E9E",
                RippleOpacitySecondary = 0.2,
                RippleOpacity = 0.1,
                HoverOpacity = 0.06,
                Surface = "rgba(255,255,255,1)",
                Background = "rgba(255,255,255,1)",
                Info = "rgba(33,150,243,1)",
                Dark = "rgba(66,66,66,1)",
                BackgroundGray = "rgba(245,245,245,1)",
                DrawerBackground = "rgba(255,255,255,1)",
                AppbarBackground = "rgba(89,74,226,1)",
                Black = "rgba(89,74,226,1)",
                TextPrimary = "rgba(66,66,66,1)",
                AppbarText = "rgba(255,255,255,1)",
                TextSecondary = "rgba(0,0,0,0.5372549019607843)",
                DrawerText = "rgba(66,66,66,1)",
                DrawerIcon = "rgba(97,97,97,1)",
                LinesInputs = "rgba(189,189,189,1)",
                ActionDisabled = "rgba(0,0,0,0.25882352941176473)",
                TextDisabled = "rgba(0,0,0,0.3764705882352941)",
                TableStriped = "rgba(0,0,0,0.0196078431372549)",
                Divider = "rgba(224,224,224,1)",
                LinesDefault = "rgba(0,0,0,0.11764705882352941)",
                ActionDisabledBackground = "rgba(0,0,0,0.11764705882352941)",
                TableLines = "rgba(224,224,224,1)",
                DividerLight = "rgba(0,0,0,0.8)",
                Warning = "rgba(255,152,0,1)",
                Error = "rgba(244,67,54,1)",
                ActionDefault = "rgba(0,0,0,0.5372549019607843)",
                Primary = "rgba(89,74,226,1)",
                Success = "rgba(0,200,83,1)",
                InfoLighten = "rgb(71,167,245)",
                PrimaryDarken = "rgb(62,44,221)",
                SuccessDarken = "rgb(0,163,68)",
                DarkLighten = "rgb(87,87,87)",
                WarningLighten = "rgb(255,167,36)",
                ErrorLighten = "rgb(246,96,85)",
                ErrorDarken = "rgb(242,28,13)",
                DarkDarken = "rgb(46,46,46)",
                WarningDarken = "rgb(214,129,0)",
                PrimaryLighten = "rgb(118,106,231)",
                SuccessLighten = "rgb(0,235,98)",
                InfoDarken = "rgb(12,128,223)",
            },
            PaletteDark = new PaletteDark()
            {
                Surface = "rgba(55,55,64,1)",
                Background = "rgba(50,51,61,1)",
                Info = "rgba(50,153,255,1)",
                Dark = "rgba(39,39,47,1)",
                BackgroundGray = "rgba(39,39,47,1)",
                DrawerBackground = "rgba(39,39,47,1)",
                AppbarBackground = "rgba(39,39,47,1)",
                Black = "rgba(39,39,47,1)",
                TextPrimary = "rgba(255,255,255,0.6980392156862745)",
                AppbarText = "rgba(255,255,255,0.6980392156862745)",
                TextSecondary = "rgba(255,255,255,0.4980392156862745)",
                DrawerText = "rgba(255,255,255,0.4980392156862745)",
                DrawerIcon = "rgba(255,255,255,0.4980392156862745)",
                LinesInputs = "rgba(255,255,255,0.2980392156862745)",
                ActionDisabled = "rgba(255,255,255,0.25882352941176473)",
                TextDisabled = "rgba(255,255,255,0.2)",
                TableStriped = "rgba(255,255,255,0.2)",
                Divider = "rgba(255,255,255,0.11764705882352941)",
                LinesDefault = "rgba(255,255,255,0.11764705882352941)",
                ActionDisabledBackground = "rgba(255,255,255,0.11764705882352941)",
                TableLines = "rgba(255,255,255,0.11764705882352941)",
                DividerLight = "rgba(255,255,255,0.058823529411764705)",
                Warning = "rgba(255,168,0,1)",
                Error = "rgba(246,78,98,1)",
                ActionDefault = "rgba(173,173,177,1)",
                Primary = "rgba(119,107,231,1)",
                Success = "rgba(11,186,131,1)",
                InfoLighten = "rgb(92,173,255)",
                PrimaryDarken = "rgb(90,75,226)",
                SuccessDarken = "rgb(9,154,108)",
                DarkLighten = "rgb(56,56,67)",
                WarningLighten = "rgb(255,182,36)",
                ErrorLighten = "rgb(248,119,134)",
                ErrorDarken = "rgb(244,47,70)",
                DarkDarken = "rgb(23,23,28)",
                WarningDarken = "rgb(214,143,0)",
                PrimaryLighten = "rgb(151,141,236)",
                SuccessLighten = "rgb(13,222,156)",
                InfoDarken = "rgb(10,133,255)",
            },
            LayoutProperties = new LayoutProperties()
            {
                AppbarHeight = "64px",
                DefaultBorderRadius = "4px",
                DrawerMiniWidthLeft = "56px",
                DrawerMiniWidthRight = "56px",
                DrawerWidthLeft = "240px",
                DrawerWidthRight = "240px",
            },
            Typography = new Typography()
            {
                Default = new DefaultTypography
                {
                    FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"],
                    FontWeight = "400",
                    FontSize = ".875rem",
                    LineHeight = "1.43",
                    LetterSpacing = ".01071em",
                    TextTransform = "none",
                },
                H1 = new H1Typography
                {
                    FontWeight = "300",
                    FontSize = "6rem",
                    LineHeight = "1.167",
                    LetterSpacing = "-.01562em",
                    TextTransform = "none",
                },
                H2 = new H2Typography
                {
                    FontWeight = "300",
                    FontSize = "3.75rem",
                    LineHeight = "1.2",
                    LetterSpacing = "-.00833em",
                    TextTransform = "none",
                },
                H3 = new H3Typography
                {
                    FontWeight = "400",
                    FontSize = "3rem",
                    LineHeight = "1.167",
                    LetterSpacing = "0",
                    TextTransform = "none",
                },
                H4 = new H4Typography
                {
                    FontWeight = "400",
                    FontSize = "2.125rem",
                    LineHeight = "1.235",
                    LetterSpacing = ".00735em",
                    TextTransform = "none",
                },
                H5 = new H5Typography
                {
                    FontWeight = "400",
                    FontSize = "1.5rem",
                    LineHeight = "1.334",
                    LetterSpacing = "0",
                    TextTransform = "none",
                },
                H6 = new H6Typography
                {
                    FontWeight = "500",
                    FontSize = "1.25rem",
                    LineHeight = "1.6",
                    LetterSpacing = ".0075em",
                    TextTransform = "none",
                },
                Subtitle1 = new Subtitle1Typography
                {
                    FontWeight = "400",
                    FontSize = "1rem",
                    LineHeight = "1.75",
                    LetterSpacing = ".00938em",
                    TextTransform = "none",
                },
                Subtitle2 = new Subtitle2Typography
                {
                    FontWeight = "500",
                    FontSize = ".875rem",
                    LineHeight = "1.57",
                    LetterSpacing = ".00714em",
                    TextTransform = "none",
                },
                Body1 = new Body1Typography
                {
                    FontWeight = "400",
                    FontSize = "1rem",
                    LineHeight = "1.5",
                    LetterSpacing = ".00938em",
                    TextTransform = "none",
                },
                Body2 = new Body2Typography
                {
                    FontWeight = "400",
                    FontSize = ".875rem",
                    LineHeight = "1.43",
                    LetterSpacing = ".01071em",
                    TextTransform = "none",
                },
                Button = new ButtonTypography
                {
                    FontWeight = "500",
                    FontSize = ".875rem",
                    LineHeight = "1.75",
                    LetterSpacing = ".02857em",
                    TextTransform = "uppercase",
                },
                Caption = new CaptionTypography
                {
                    FontWeight = "400",
                    FontSize = ".75rem",
                    LineHeight = "1.66",
                    LetterSpacing = ".03333em",
                    TextTransform = "none",
                },
                Overline = new OverlineTypography
                {
                    FontWeight = "400",
                    FontSize = ".75rem",
                    LineHeight = "2.66",
                    LetterSpacing = ".08333em",
                    TextTransform = "none",
                },
            },
            ZIndex = new ZIndex()
            {
                AppBar = 1300,
                Dialog = 1400,
                Drawer = 1100,
                Popover = 1200,
                Snackbar = 1500,
                Tooltip = 1600,
            },
        };

        public static readonly MudTheme ThemeV2 = new()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = "#594AE2",
                Secondary = "#FF4081",
                Tertiary = "#1EC8A5",
                Info = "#2196F3",
                Success = "#00C853",
                Warning = "#FF9800",
                Error = "#F44336",
                Dark = "#424242",

                Background = "#FFFFFF",
                Surface = "#FFFFFF",
                BackgroundGray = "#F5F5F5",
                DrawerBackground = "#FFFFFF",
                AppbarBackground = "#594AE2",
                AppbarText = "#FFFFFF",

                TextPrimary = "#424242",
                TextSecondary = "rgba(0,0,0, 0.54)",
                TextDisabled = "rgba(0,0,0, 0.38)",

                ActionDefault = "rgba(0,0,0, 0.54)",
                ActionDisabled = "rgba(0,0,0, 0.26)",
                ActionDisabledBackground = "rgba(0,0,0, 0.12)",

                Divider = "#E0E0E0",
                DividerLight = "rgba(0,0,0, 0.08)",
                TableLines = "#E0E0E0",
                LinesDefault = "rgba(0,0,0, 0.12)",
                LinesInputs = "#BDBDBD",

                Black = "#000000",
                White = "#FFFFFF",
            },
            PaletteDark = new PaletteDark()
            {
                Primary = "#776BE7",
                Success = "#0BBA83",
                Surface = "#1E1E2C",
                Background = "#1A1A27",
                BackgroundGray = "#151521",
                AppbarBackground = "#1A1A27",
                AppbarText = "rgba(255,255,255, 0.70)",
                TextPrimary = "rgba(255,255,255, 0.70)",
                TextSecondary = "rgba(255,255,255, 0.50)",
                ActionDefault = "#ADADB1",
                Divider = "rgba(255,255,255, 0.12)",
                DividerLight = "rgba(255,255,255, 0.06)",
                TableLines = "rgba(255,255,255, 0.12)",
                LinesDefault = "rgba(255,255,255, 0.12)",
                LinesInputs = "rgba(255,255,255, 0.30)",
                DrawerBackground = "#1A1A27",
                DrawerText = "rgba(255,255,255, 0.50)",
                DrawerIcon = "rgba(255,255,255, 0.50)",
            },
            LayoutProperties = new LayoutProperties()
            {
                AppbarHeight = "64px",
                DefaultBorderRadius = "4px",
                DrawerMiniWidthLeft = "56px",
                DrawerMiniWidthRight = "56px",
                DrawerWidthLeft = "240px",
                DrawerWidthRight = "240px",
            },
            Typography = new Typography()
            {
                Default = new DefaultTypography
                {
                    FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"],
                    FontWeight = "400",
                    FontSize = ".875rem",
                    LineHeight = "1.43",
                    LetterSpacing = ".01071em",
                    TextTransform = "none",
                },
                H1 = new H1Typography
                {
                    FontWeight = "300",
                    FontSize = "6rem",
                    LineHeight = "1.167",
                    LetterSpacing = "-.01562em",
                    TextTransform = "none",
                },
                H2 = new H2Typography
                {
                    FontWeight = "300",
                    FontSize = "3.75rem",
                    LineHeight = "1.2",
                    LetterSpacing = "-.00833em",
                    TextTransform = "none",
                },
                H3 = new H3Typography
                {
                    FontWeight = "400",
                    FontSize = "3rem",
                    LineHeight = "1.167",
                    LetterSpacing = "0",
                    TextTransform = "none",
                },
                H4 = new H4Typography
                {
                    FontWeight = "400",
                    FontSize = "2.125rem",
                    LineHeight = "1.235",
                    LetterSpacing = ".00735em",
                    TextTransform = "none",
                },
                H5 = new H5Typography
                {
                    FontWeight = "400",
                    FontSize = "1.5rem",
                    LineHeight = "1.334",
                    LetterSpacing = "0",
                    TextTransform = "none",
                },
                H6 = new H6Typography
                {
                    FontWeight = "500",
                    FontSize = "1.25rem",
                    LineHeight = "1.6",
                    LetterSpacing = ".0075em",
                    TextTransform = "none",
                },
                Subtitle1 = new Subtitle1Typography
                {
                    FontWeight = "400",
                    FontSize = "1rem",
                    LineHeight = "1.75",
                    LetterSpacing = ".00938em",
                    TextTransform = "none",
                },
                Subtitle2 = new Subtitle2Typography
                {
                    FontWeight = "500",
                    FontSize = ".875rem",
                    LineHeight = "1.57",
                    LetterSpacing = ".00714em",
                    TextTransform = "none",
                },
                Body1 = new Body1Typography
                {
                    FontWeight = "400",
                    FontSize = "1rem",
                    LineHeight = "1.5",
                    LetterSpacing = ".00938em",
                    TextTransform = "none",
                },
                Body2 = new Body2Typography
                {
                    FontWeight = "400",
                    FontSize = ".875rem",
                    LineHeight = "1.43",
                    LetterSpacing = ".01071em",
                    TextTransform = "none",
                },
                Button = new ButtonTypography
                {
                    FontWeight = "500",
                    FontSize = ".875rem",
                    LineHeight = "1.75",
                    LetterSpacing = ".02857em",
                    TextTransform = "uppercase",
                },
                Caption = new CaptionTypography
                {
                    FontWeight = "400",
                    FontSize = ".75rem",
                    LineHeight = "1.66",
                    LetterSpacing = ".03333em",
                    TextTransform = "none",
                },
                Overline = new OverlineTypography
                {
                    FontWeight = "400",
                    FontSize = ".75rem",
                    LineHeight = "2.66",
                    LetterSpacing = ".08333em",
                    TextTransform = "none",
                },
            },
            ZIndex = new ZIndex()
            {
                AppBar = 1300,
                Dialog = 1400,
                Drawer = 1100,
                Popover = 1200,
                Snackbar = 1500,
                Tooltip = 1600,
            },
        };
    }
}
