using System.Windows.Input;

namespace Msm.Core.Wpf.Commands
{
    /// <summary>
    ///     <para>
    ///         The <see cref="RelayCommand" /> allows to bind command which action is provided by lambda expression or
    ///         function delegate.
    ///     </para>
    /// </summary>
    public class RelayCommand
        : ICommand
    {
        /// <summary>
        ///     Action to perform on command execute.
        /// </summary>
        private readonly Action<object?> _execute;

        /// <summary>
        ///     Checks if command can be executed.
        /// </summary>
        private readonly Func<object?, bool>? _canExecute;

        /// <summary>
        ///     <see cref="CanExecuteChanged" /> backing field.
        /// </summary>
        private EventHandler? _canExecuteChanged;

        /// <summary>
        ///     Initializes new instance of the <see cref="RelayCommand" /> class.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = _ => execute();

            if (canExecute != null)
            {
                _canExecute = _ => canExecute();
            }
        }

        /// <summary>
        ///     Initializes new instance of the <see cref="RelayCommand" /> class.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        ///     Notifies listeners when <see cref="CanExecute" /> has changed.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add
            {
                _canExecuteChanged += value;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                _canExecuteChanged -= value;
                CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        ///     Checks if command can be executed.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>True if command can be executed or no check has been performed.</returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        ///     Executes the command.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        ///     Implicit cast from delegate.
        /// </summary>
        /// <param name="execute">Delegate that will be used as action to execute.</param>
        public static implicit operator RelayCommand(Action<object?> execute)
        {
            return new(execute);
        }

        /// <summary>
        ///     Implicit cast from delegate.
        /// </summary>
        /// <param name="execute">Delegate that will be used as action to execute.</param>
        public static implicit operator RelayCommand(Action execute)
        {
            return new(execute);
        }

        /// <summary>
        ///     Implicit cast from delegate pair.
        /// </summary>
        /// <param name="pair">Pair of delegates.</param>
        public static implicit operator RelayCommand((Action<object?> execute, Func<object?, bool> tryExecute) pair)
        {
            return new(pair.execute, pair.tryExecute);
        }

        /// <summary>
        ///     Raises <see cref="CanExecuteChanged" /> event.
        /// </summary>
        public void OnCanExecuteChanged()
        {
            _canExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class RelayCommand<TArgument>
        : RelayCommand
    {
        /// <summary>
        ///     Initializes new instance of the <see cref="RelayCommand{TArgument}" /> class.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action<TArgument?> execute, Func<TArgument?, bool>? canExecute = null)
            : base(
                o => execute(TryCastArgument(o)),
                canExecute is null
                    ? null
                    : o => canExecute.Invoke(TryCastArgument(o)))
        {
        }

        /// <summary>
        ///     Implicit cast from delegate.
        /// </summary>
        /// <param name="execute">Delegate that will be used as action to execute.</param>
        public static implicit operator RelayCommand<TArgument?>(Action<TArgument?> execute)
        {
            return new(execute);
        }

        /// <summary>
        ///     Implicit cast from delegate pair.
        /// </summary>
        /// <param name="pair">Pair of delegates.</param>
        public static implicit operator RelayCommand<TArgument?>((Action<TArgument?> execute, Func<TArgument?, bool> tryExecute) pair)
        {
            return new(pair.execute, pair.tryExecute);
        }

        /// <summary>
        /// Safe casts <paramref name="obj"/> to <typeparamref name="TArgument"/> if possible, or fallbacks to default otherwise.
        /// </summary>
        /// <param name="obj">Value to cast.</param>
        /// <returns>Value if cast succeeds or default value if cast fails.</returns>
        private static TArgument? TryCastArgument(object? obj)
        {
            return obj is TArgument arg
                ? arg
                : default;
        }
    }
}
