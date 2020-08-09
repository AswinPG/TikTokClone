﻿using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TikTokClone.Controls
{
    public class MarqueeLabel : Label
    {
        public static readonly BindableProperty AnimatedTextProperty =
            BindableProperty.Create(
                nameof(AnimatedText),
                typeof(string),
                typeof(MarqueeLabel),
                string.Empty,
                BindingMode.OneWay,
                propertyChanged: OnAnimatedTextChanged
            );

        public string AnimatedText
        {
            get { return (string)GetValue(AnimatedTextProperty); }
            set { SetValue(AnimatedTextProperty, value); }
        }

        private static void OnAnimatedTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MarqueeLabel label)
                label.Text = (string)newValue;
        }

        private int _totalLetters = 0;
        private CancellationToken _token;

        public async Task StartAnimationAsync(CancellationToken token)
        {
            _token = token;
            _totalLetters = Text.Length;
            await MoveLettersAsync();
        }

        private async Task MoveLettersAsync()
        {
            await MoveTextLettersAsync();
            await MoveSpaceCharactersAsync();
            RestoreOriginalText();
            await MoveLettersAsync();
        }

        private async Task MoveTextLettersAsync()
        {
            for (var letterIndex = 0; letterIndex < _totalLetters; letterIndex++)
            {
                _token.ThrowIfCancellationRequested();

                var charsToRemove = GetFirstLetterToRemove();
                var isFirstLetter = letterIndex == 0;
                var textWithRemovedLetterAtEnd = AddLetterToTheEnd(charsToRemove, isFirstLetter);
                var newText = RemoveFirstLetter(textWithRemovedLetterAtEnd);

                SetNewText(newText);
                await Task.Delay(100);
            }
        }

        private async Task MoveSpaceCharactersAsync()
        {
            for (var _ = 0; _ < SpaceCharacters.Length; _++)
            {
                _token.ThrowIfCancellationRequested();

                var charsToRemove = GetFirstLetterToRemove();
                var isFirstLetter = false;
                var textWithRemovedLetterAtEnd = AddLetterToTheEnd(charsToRemove, isFirstLetter);
                var newText = RemoveFirstLetter(textWithRemovedLetterAtEnd);

                SetNewText(newText);
                await Task.Delay(100);
            }
        }

        private string GetFirstLetterToRemove() => Text.Substring(0, 1);

        private const string SpaceCharacters = "    ";

        private string AddLetterToTheEnd(string letter, bool isFirstLetter)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(Text);

            if (isFirstLetter)
                stringBuilder.Append(SpaceCharacters);

            stringBuilder.Append(letter);

            return stringBuilder.ToString();
        }

        private string RemoveFirstLetter(string text) => text.Substring(1);

        public void RestoreOriginalText() => SetNewText(AnimatedText);

        public void SetNewText(string newText) => Device.BeginInvokeOnMainThread(() => Text = newText);
    }
}
