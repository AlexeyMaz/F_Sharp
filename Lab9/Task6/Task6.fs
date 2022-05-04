open System.Windows
open System.Windows.Controls
open System.Windows.Markup
open System

let onButtonClick eventArgs =
    System.Diagnostics.Process.Start("https://fb.ru/misc/i/gallery/38575/3156511.jpg") |> ignore

let onSliderChange (button: Button) (eventArgs: RoutedPropertyChangedEventArgs<float>) =
    button.Width <- 20.0 + eventArgs.NewValue * 10.0

let parseWindow(mwXaml) =
    let xamlObj=XamlReader.Parse(mwXaml)
    xamlObj :?> Window

let initWindow() =
    let xaml = "
        <Window
        	xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
        	xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' Title='F# WPF' Height='280' Width='320'>
        	<Grid>
        		<Grid.ColumnDefinitions>
        			<ColumnDefinition Width='320*' />
        		</Grid.ColumnDefinitions>
                <Grid.RowDefinitions>
                    <RowDefinition Height='140*' />
                    <RowDefinition Height='140*' />
                </Grid.RowDefinitions>
                
                <Button Name='button1' Grid.Column='0' Grid.Row='0' Content='Маслины' Height='23' HorizontalAlignment='Center' VerticalAlignment='Center' Width='20' />
        		<Slider Name='slider1' Grid.Column='0' Grid.Row='1' VerticalAlignment='Top' HorizontalAlignment='Center' Minimum='0' Maximum='10' Width='100' />
        		
        	</Grid>
        </Window>
    "

    let win = parseWindow xaml

    let button = win.FindName("button1") :?> Button
    let slider = win.FindName("slider1") :?> Slider
    button.Click.Add onButtonClick
    slider.ValueChanged.Add (onSliderChange button)

    win

[<EntryPoint>]
[<STAThread>] 
let main argv =
    let win = initWindow()
    ignore <| (new Application()).Run win
    0