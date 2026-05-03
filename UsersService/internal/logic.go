package internal

import (
	"math/rand"

	"github.com/go-faker/faker/v4"
)

func generateRandomUser() (User, error) {
	var user User
	err := faker.FakeData(&user)
	if err != nil {
		return User{}, err
	}
	return user, nil
}

func generateRandomUsers(n int) ([]User, error) {
	users := make([]User, n)
	for i := range users {
		user, err := generateRandomUser()
		if err != nil {
			return nil, err
		}
		users[i] = user
	}
	return users, nil
}

func generateRandomProducts(n int) ([]Product, error) {
	items := make([]Product, n)
	for i := range items {
		if err := faker.FakeData(&items[i]); err != nil {
			return nil, err
		}
	}
	return items, nil
}

func generateRandomOrder() (Order, error) {
	var order Order
	if err := faker.FakeData(&order); err != nil {
		return Order{}, err
	}
	lineCount := 2 + rand.Intn(3)
	order.Lines = make([]OrderLine, lineCount)
	for i := range order.Lines {
		var line OrderLine
		if err := faker.FakeData(&line); err != nil {
			return Order{}, err
		}
		line.Quantity = 1 + rand.Intn(10)
		order.Lines[i] = line
	}
	return order, nil
}

func generateRandomTransactions(n int) ([]Transaction, error) {
	items := make([]Transaction, n)
	for i := range items {
		if err := faker.FakeData(&items[i]); err != nil {
			return nil, err
		}
	}
	return items, nil
}

func generateRandomAuditEvents(n int) ([]AuditEvent, error) {
	items := make([]AuditEvent, n)
	for i := range items {
		if err := faker.FakeData(&items[i]); err != nil {
			return nil, err
		}
	}
	return items, nil
}

func generateRandomDevices(n int) ([]Device, error) {
	items := make([]Device, n)
	for i := range items {
		if err := faker.FakeData(&items[i]); err != nil {
			return nil, err
		}
	}
	return items, nil
}
