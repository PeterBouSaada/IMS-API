module ApplicationHelper
  def active_class(path)
    if request.path == path
      'active'
    else
      ''
    end
  end
  def is_part_of_path(path)
    if request.path.start_with?(path)
      true
    else
      false
    end
  end
end
