Rails.application.routes.draw do
  devise_for :users, controllers: {
    registrations: "users/registrations"
  }
  root to: "main#not_found"
end
