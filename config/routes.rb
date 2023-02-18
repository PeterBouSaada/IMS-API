Rails.application.routes.draw do
  resources :parts
  resources :machines
  devise_for :users
  root to: "home#index"
end
