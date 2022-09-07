Rails.application.routes.draw do
  namespace :api do
    namespace :v1 do
      devise_for :users
    end
  end
  
  root to: "main#not_found"
end
