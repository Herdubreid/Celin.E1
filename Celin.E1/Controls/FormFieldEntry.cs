namespace Celin.Controls;

public class FormFieldEntry<T> : ContentView
    where T : IConvertible
{
    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(
            nameof(Value),
            typeof(AIS.FormField<T>),
            typeof(FormFieldEntry<T>),
            null,
            BindingMode.TwoWay);
    public static readonly BindableProperty CanEditProperty =
        BindableProperty.Create(
            nameof(CanEdit),
            typeof(bool),
            typeof(FormFieldEntry<T>),
            false);

    public bool CanEdit
    {
        get => (bool)GetValue(CanEditProperty);
        set => SetValue(CanEditProperty, value);
    }
    public AIS.FormField<T> Value
    {
        get => (AIS.FormField<T>)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
    Label _label { get; }
    Entry _entry { get; }
    public FormFieldEntry()
    {
        _label = new Label { FontSize = 12, Margin = new Thickness(14,0) };
        _label.SetBinding(Label.TextProperty, new Binding($"{nameof(Value)}.{nameof(Value.title)}", source: this));
        _entry = new Entry();
        _entry.SetBinding(Entry.TextProperty, new Binding($"{nameof(Value)}.{nameof(Value.value)}", source: this));
        _entry.SetBinding(IsEnabledProperty, new Binding(nameof(CanEdit), source: this));
        Content = new VerticalStackLayout
        {
            Margin = 10,
            Children = { _label, _entry }
        };
    }
}

public class FormFieldStringEntry : FormFieldEntry<string> { }
public class FormFieldIntEntry : FormFieldEntry<int> { }
public class FormFieldDecimalEntry : FormFieldEntry<decimal> { }