package internal

import "github.com/go-faker/faker/v4"

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
