## Задание:

**Реализовать модуль для работы с кошельком пользователя, позволяющий:**
- Получать информацию о балансе доступных валют.
- Изменять значение конкретной валюты.
- Сохранять и загружать текущее состояние кошелька.

Кошелек должен быть реализован отдельным логическим модулем.
(Опционально) Он может быть подключен как сабмодуль .GIT, пакет (Unity Package Manager).

**Реализовать тестовое приложение со следующим функционалом:**
- Отображение количества монет и кристаллов у пользователя. Дать возможность обнулять и инкрементить текущий баланс каждой из этих валют.
- Сохранение и загрузка данных в PlayerPrefs по заданному ключу в текстовом виде, в файл с заданным именем в текстовом и бинарном виде, 
(опционально) на сервер с заданными параметрами.

**Визуализация произвольная.**
Оцениваться будет следование принципам и уместность использования шаблонов проектирования для решения конкретных подзадач. 
Особое внимание стоит уделить простоте и удобству использования API модуля, организации связей между классами внутри модуля, 
а также расширяемости готового решения (добавление новых типов валют, методов сохранения, сериализации).

## Инструкция:

1) Добавить на GameObject сцены компонент WallemManager. 
WallemManager может быть только в единстценном экземпляре. В противном случае вы получите ошибку в инспекторе.

2) Задать необходимое количество ключей валют в (Window -> Simplewallet -> Settings). 
При сохранении будет сформирован enum CurrencyType, который обеспечивает удобный доступ к валютам SimpleWallet из C# скриптов.

3) Настроить атрибуты валют (отображаемое имя, иконка) в компоненте WallemManager

4) Выбрать тип сохранения/чтения баланса кошелька в компоненте WallemManager

## API

Modify SimpleWallet currency balance:
```
public static WalletManager.ModifyCurrencyValue(CurrencyType type, int amount)

// Example: Add 10 Coins
WalletManager.ModifyCurrencyValue(CurrencyType.Coins, 10);

// Example: Substract 10 Coins
WalletManager.ModifyCurrencyValue(CurrencyType.Coins, -10)

```

Set SimpleWallet currency balance:
```
public static WalletManager.SetCurrencyValue(CurrencyType type, int amount)

// Example: Set Coins to 100
WalletManager.SetCurrencyValue(CurrencyType.Coins, 100);
```

Nullify SimpleWallet balance:
```
public static WalletManager.NullifyWalletBalance()

// Example: Set all SimpleWallet currencies ballance to 0
WalletManager.NullifyWalletBalance();
```

Save SimpleWallet balance:
```
public static WalletManager.SaveBalance()

// Example: Save SimpleWallet beetween game sessions
WalletManager.SaveBalance();
```

Save SimpleWallet balance:
```
public static WalletManager.LoadBalance()

// Example: Load SimpleWallet balance from previously saved
WalletManager.LoadBalance();
```

Any time SimpleWallet currency is modified, WalletManager.OnCurrencyValueChanged is fired.

```
public static WalletManager.LoadBalance()

// Example: Load SimpleWallet balance from previously saved
 private void OnEnable()
        {
            WalletManager.OnCurrencyValueChanged += OnCurrencyValueChangedHandler;
        }


        private void OnDisable()
        {
            WalletManager.OnCurrencyValueChanged -= OnCurrencyValueChangedHandler;
        }
        
  private void OnCurrencyValueChangedHandler(object sender, CurrencyEventArgs e)
        {
            Debug.Log(e.type);
            Debug.Log(e.OldValue);
            Debug.Log(e.Value);
        }
```

