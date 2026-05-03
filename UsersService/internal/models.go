package internal

type User struct {
	FirstName string `faker:"first_name"`
	LastName  string `faker:"last_name"`
}

type Product struct {
	Sku   string  `faker:"uuid_hyphenated"`
	Name  string  `faker:"word"`
	Price float64 `faker:"amount"`
}

type OrderLine struct {
	Sku      string  `faker:"uuid_hyphenated"`
	Quantity int     `faker:"-"`
	Price    float64 `faker:"amount"`
}

type Order struct {
	Id       string      `faker:"uuid_hyphenated"`
	Customer string      `faker:"name"`
	Lines    []OrderLine `faker:"-"`
}

type Transaction struct {
	Id     string  `faker:"uuid_hyphenated"`
	From   string  `faker:"name"`
	To     string  `faker:"name"`
	Amount float64 `faker:"amount"`
	At     string  `faker:"timestamp"`
}

type AuditEvent struct {
	Id     string `faker:"uuid_hyphenated"`
	Actor  string `faker:"username"`
	Action string `faker:"oneof: created, updated, deleted, viewed, exported"`
	At     string `faker:"timestamp"`
}

type Device struct {
	MacAddress string `faker:"mac_address"`
	Firmware   string `faker:"oneof: 1.0.0, 1.2.3, 2.0.1, 3.4.5, 4.1.0"`
}
