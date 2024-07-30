from typing import Callable, Optional

class Account():
    def __init__(
            self,
            name: str,
            starting_balance: float
    ) -> None:
        self._balance: float = starting_balance
        self._name: str = name
    
    @property
    def balance(self) -> float:
        return self._balance
    
    @property
    def name(self) -> str:
        return self._name
    
    def deposit(self, amount: float) -> bool:
        if amount > 0:
            self._balance += amount
            return True
        return False
    
    def print(self) -> None:
        print(f'Account: {self.name} - (${self.balance:.2f})')

    def withdraw(self, amount: float) -> bool:
        if amount > 0 and amount <= self.balance:
            self._balance -= amount
            return True
        return False
    
def do_create(accounts: list[Account]) -> None:
    balance: float = -1
    name: str = ''
    while name == '':
        name = input('Input a Name for the Account: ').strip()
    while balance < 0:
        try:
            balance = float(input(f'Input the Starting Balance for {name}: '))
        except ValueError:
            print('Invalid Input. Please enter a number.')
    accounts.append(Account(name, balance))

def do_deposit(accounts: list[Account]) -> None:
    account: Optional[Account] = get_account(accounts)
    if account is None: return None
    balance: float = -1
    while balance < 0:
        try:
            balance = float(
                input(f'Input the Deposit Amount for {account.name}: ')
            )
        except ValueError:
            print('Invalid Input. Please enter a number.')
    if not account.deposit(balance):
        print('Error: Failed to Deposit')

def do_print(accounts: list[Account]) -> None:
    print('Account Information')
    for account in accounts: account.print()

def do_withdraw(accounts: list[Account]) -> None:
    account: Optional[Account] = get_account(accounts)
    if account is None: return None
    balance: float = -1
    while balance < 0:
        try:
            balance = float(
                input(f'Input the Withdraw Amount for {account.name}: ')
            )
        except ValueError:
            print('Invalid Input. Please enter a number.')
    if not account.withdraw(balance):
        print('Error: Failed to Withdraw')
    
def get_account(accounts: list[Account]) -> Optional[Account]:
    name: str = input('Input the Account Name to Get: ')
    a: Optional[Account] = next(
        iter([
            a
            for a in accounts
            if a.name.lower() == name.lower()
        ]),
        None
    )
    if a is None: print('No Account Found')
    return a
    
def get_user_choice() -> int:
    print(
        '.~~~~~~~~~~~~~~~~~~~~~~~~~~.\n' \
        + '|  9.1 - Another Language  |\n' \
        + '|--------------------------|\n' \
        + '| 1. Create Account        |\n' \
        + '| 2. Deposit Into Account  |\n' \
        + '| 3. Withdraw from Account |\n' \
        + '| 4. Check Account Info    |\n' \
        + '| 5. Exit                  |\n' \
        + '|__________________________|'
    )
    while True:
        try:
            val =  int(input())
            if val in [1, 2, 3, 4, 5]: return val
            raise ValueError('Invalid Value')
        except:
            print('Invalid Input, Try Again')
    
def main():
    accounts: list[Account] = []
    choice_functions: dict[int, Callable[[list[Account]], None]] = {
        1: do_create,
        2: do_deposit,
        3: do_withdraw,
        4: do_print,
        5: lambda x: None,
    }

    while True:
        choice: int = get_user_choice()
        choice_functions[choice](accounts)
        if choice == 5: break
    print('\n\n\nDone')

if __name__ == '__main__': main()
